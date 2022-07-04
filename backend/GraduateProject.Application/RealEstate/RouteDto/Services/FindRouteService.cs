using AutoMapper;
using AutoMapper.QueryableExtensions;
using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Z.EntityFramework.Plus;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class FindRouteService : IFindRouteService
{
    private readonly IVertexRepository _vertexRepository;
    private readonly IStopRepository _stopRepository;
    private readonly ConfigDistance _configDistance;
    private readonly IMapper _mapper;
    private readonly IAStarService _aStarService;
    private readonly ILogger<FindRouteService> _logger;
    private readonly IPathHistoryRepository _pathHistoryRepository;


    public FindRouteService(IVertexRepository vertexRepository, IOptionsSnapshot<ConfigDistance> configDistance, IStopRepository stopRepository, IMapper mapper,
        IAStarService aStarService, ILogger<FindRouteService> logger, IPathHistoryRepository pathHistoryRepository)
    {
        _vertexRepository = vertexRepository;
        _configDistance = configDistance.Value;
        _stopRepository = stopRepository;
        _mapper = mapper;
        _aStarService = aStarService;
        _logger = logger;
        _pathHistoryRepository = pathHistoryRepository;
    }

    public async Task<RouteResponseDtoV2?> GetRoute(FindRouteRequestDto request)
    {
        // var pathHistory = await _pathHistoryRepository.Queryable().FirstOrDefaultAsync(x =>
        //     x.StartLat.Equals(request.StartPoint.Lat) &&
        //     x.StartLng.Equals(request.StartPoint.Lng) &&
        //     x.EndLat.Equals(request.EndPoint.Lat) &&
        //     x.EndLng.Equals(request.EndPoint.Lng)
        // );
        // if (pathHistory is not null)
        // {
        //     if (pathHistory.IsError) throw new Exception(pathHistory.ErrorMessage);
        //     return JsonConvert.DeserializeObject<RouteResponseDtoV2>(pathHistory.JsonPath ?? "{}");
        // }

        try
        {
            var stops = await _stopRepository.Queryable().ProjectTo<StopDto>(_mapper.ConfigurationProvider).ToListAsync();
            var blackListStartPoint = new List<StopDto>();
            var blackListEndPoint = new List<StopDto>();

            var listResultPaths = new List<RouteResponseDtoV2?>();
            RouteResponseDtoV2 result = null;
            while (true)
            {
                var newResultPaths = await GeneratePathResult(request, stops, blackListStartPoint, blackListEndPoint);
                if (newResultPaths is null) break;
                listResultPaths.Add(newResultPaths);
                if (newResultPaths?.Weight <= 1.5) return newResultPaths;
            }

            return listResultPaths.OrderBy(x => x?.Weight).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await _pathHistoryRepository.AddAsync(new PathHistory()
            {
                StartLat = request.StartPoint.Lat,
                StartLng = request.StartPoint.Lng,
                EndLat = request.EndPoint.Lat,
                EndLng = request.EndPoint.Lng,
                IsError = true,
                ErrorMessage = e.Message
            }, true);
            throw;
        }
    }

    private async Task<RouteResponseDtoV2?> GeneratePathResult(FindRouteRequestDto request, List<StopDto> stops, List<StopDto> blackListStartPoint,
        List<StopDto> blackListEndPoint)
    {
        var stopPaths = await GetBusStopList(request, stops, blackListStartPoint, blackListEndPoint);
        if (stopPaths is null) return null;
        var positions = stopPaths.Select(x => x.Position).ToList();


        double distanceStraightFromStartToEnd = CalculateUtil.Distance(request.StartPoint, request.EndPoint);

        var resultPos = new List<(Position, Position, string)>();
        for (int i = 0; i < stopPaths.Count - 1; i++)
        {
            var currStop = stops.First(x => x.Id == stopPaths[i].Id);
            var nextStop = stops.First(x => x.Id == stopPaths[i + 1].Id);

            var currStopRoutes = currStop.Routes.Split(",").Select(x => x.Trim());
            var nextStopRoutes = nextStop.Routes.Split(",").Select(x => x.Trim());

            var intersectionList = currStopRoutes.Intersect(nextStopRoutes).ToList();
            if (intersectionList.Any())
            {
                currStop.Routes = string.Join(", ", intersectionList);
                resultPos.Add((currStop.Position, nextStop.Position, "Main"));
            }
            else
            {
                resultPos.Add((currStop.Position, nextStop.Position, "Switch"));
            }

            if (i == 0)
            {
                resultPos.Add((request.StartPoint, currStop.Position, "Switch"));
            }

            if (i == stopPaths.Count - 2)
            {
                resultPos.Add((stopPaths[i + 1].Position, request.EndPoint, "Switch"));
            }
        }


        double totalDistance = 0;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            totalDistance += CalculateUtil.Distance(positions[i], positions[i + 1]);
        }

        var newResult = new RouteResponseDtoV2()
        {
            Positions = positions,
            Stops = stopPaths.Select(x => stops.First(y => y.Id == x.Id)).ToList(),
            Weight = Math.Round(totalDistance / distanceStraightFromStartToEnd, 3),
            ResultPositions = resultPos,
        };
        positions.Insert(0, request.StartPoint);
        positions.Add(request.EndPoint);
        return newResult;
    }

    private async Task GetRoutePathList(List<AStarNodeBusStop> stopPaths, double limitSearchRadius, List<Position> pathPos)
    {
        if (stopPaths.Count > 1)
        {
            var vertices = await GetVertices();
            var edges = await GetEdges();

            for (int i = 0; i < stopPaths.Count - 1; i++)
            {
                _logger.LogInformation("stopPath num: {0}/{1}", i, stopPaths.Count - 1);
                var startPointNeighborVertices = new List<VertexDto>();
                var endPointNeighborVertices = new List<VertexDto>();
                var stopStartPoint = stopPaths[i];
                var stopEndPoint = stopPaths[i + 1];
                foreach (var vertex in vertices)
                {
                    var distanceToStart = CalculateUtil.Distance(stopStartPoint.Position, vertex.Position);
                    var distanceToEnd = CalculateUtil.Distance(stopEndPoint.Position, vertex.Position);
                    if (distanceToStart <= limitSearchRadius)
                    {
                        vertex.DistanceToStart = distanceToStart;
                        vertex.DistanceToEnd = CalculateUtil.Distance(stopEndPoint.Position, vertex.Position);
                        startPointNeighborVertices.Add(vertex);
                    }

                    if (distanceToEnd <= limitSearchRadius)
                    {
                        vertex.DistanceToEnd = distanceToEnd;
                        endPointNeighborVertices.Add(vertex);
                    }
                }

                if (!startPointNeighborVertices.Any()) throw new Exception("Route nearby not found!");
                var startPoint = startPointNeighborVertices.OrderBy(x => x.DistanceToStart).First();
                var endPoint = endPointNeighborVertices.OrderBy(x => x.DistanceToEnd).First();
                var resultVertices = await _aStarService.StartAlgorithmsRoutePaths(vertices, edges, startPoint.Id, endPoint.Id);
                var pos = resultVertices.Select(x => x.Position);
                pathPos.AddRange(pos);
            }
        }
    }

    private async Task<List<AStarNodeBusStop>?> GetBusStopList(FindRouteRequestDto request, List<StopDto> stops, List<StopDto> blackListStartPoint,
        List<StopDto> blackListEndPoint)
    {
        var (listStartPoint, listEndPoint) = StartPointNeighborVertices(request, stops);
        var startPoint = listStartPoint.Where(x => !blackListStartPoint.Select(y => y.Id).Contains(x.Id)).OrderBy(x => x.DistanceToStart).FirstOrDefault();
        var endPoint = listEndPoint.OrderBy(x => x.DistanceToEnd).FirstOrDefault();
        if (startPoint is null || endPoint is null) return null;
        blackListStartPoint.AddRange(listStartPoint.Where(x => x.Lat.Equals(startPoint.Lat) && x.Lng.Equals(startPoint.Lng)));
        blackListEndPoint.Add(endPoint);
        var stopPaths = await _aStarService.StartAlgorithmsBusStopPaths(startPoint, endPoint, stops);
        return stopPaths;
    }

    private (List<StopDto>, List<StopDto>) StartPointNeighborVertices(FindRouteRequestDto request, List<StopDto> stops)
    {
        foreach (var stop in stops)
        {
            var distanceToStart = CalculateUtil.Distance(request.StartPoint, stop.Position);
            var distanceToEnd = CalculateUtil.Distance(request.EndPoint, stop.Position);
            stop.DistanceToStart = distanceToStart;
            stop.DistanceToEnd = distanceToEnd;
        }

        var listStartPoint = stops.Where(x => x.DistanceToStart <= _configDistance.Limit).ToList();
        var listEndPoint = stops.Where(x => x.DistanceToEnd <= _configDistance.Limit).ToList();
        return (listStartPoint, listEndPoint);
    }

    private async Task<List<EdgeDto>> GetEdges()
    {
        var edges = await _vertexRepository.GetEdgeQueryable().AsNoTracking()
            .Select(x => new EdgeDto()
            {
                PointAId = x.PointAId,
                PointBId = x.PointBId,
                EdgeDistance = x.Distance,
                Type = x.Type
            }).FromCacheAsync();
        return edges.ToList();
    }

    private async Task<List<VertexDto>> GetVertices()
    {
        var vertices = await _vertexRepository.Queryable().AsNoTracking()
            .Select(x => new VertexDto()
            {
                Id = x.Id,
                Lat = x.Lat,
                Lng = x.Lng,
                Rank = x.Rank,
                RouteDetailId = x.RouteDetailId
            })
            .FromCacheAsync();
        return vertices.ToList();
    }
}