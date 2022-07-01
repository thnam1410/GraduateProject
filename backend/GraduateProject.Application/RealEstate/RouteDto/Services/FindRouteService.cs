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

    public async Task<RouteResponseDtoV2> GetRoute(FindRouteRequestDto request)
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
            var stopPaths = await GetBusStopList(request, stops);
            var results = stopPaths.Select(x => x.Position).ToList();
            results.Insert(0, request.StartPoint);
            results.Add(request.EndPoint);

            double totalDistance = 0;
            for (int i = 0; i < results.Count - 1; i++)
            {
                totalDistance += CalculateUtil.Distance(results[i], results[i + 1]);
            }

            double distanceStraightFromStartToEnd = CalculateUtil.Distance(request.StartPoint, request.EndPoint);
            
            return new RouteResponseDtoV2()
            {
                Positions = results,
                Stops = stopPaths.Select(x => stops.First(y => y.Id == x.Id)).ToList(),
                Weight = Math.Round(totalDistance / distanceStraightFromStartToEnd, 3)
            };
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

    private async Task<List<AStarNodeBusStop>> GetBusStopList(FindRouteRequestDto request, List<StopDto> stops)
    {
        StartPointNeighborVertices(request, stops, out var startPoint, out var endPoint);
        if (startPoint is null) throw new Exception("Can not find nearby start point!");
        if (endPoint is null) throw new Exception("Can not find nearby end point!");
        var stopPaths = await _aStarService.StartAlgorithmsBusStopPaths(startPoint, endPoint, stops);
        return stopPaths;
    }

    private void StartPointNeighborVertices(FindRouteRequestDto request, List<StopDto> stops, out StopDto? startPoint,
        out StopDto? endPoint)
    {
        startPoint = null;
        endPoint = null;
        foreach (var stop in stops)
        {
            var distanceToStart = CalculateUtil.Distance(request.StartPoint, stop.Position);
            var distanceToEnd = CalculateUtil.Distance(request.EndPoint, stop.Position);
            stop.DistanceToStart = distanceToStart;
            stop.DistanceToEnd = distanceToEnd;
        }

        startPoint = stops.OrderBy(x => x.DistanceToStart).FirstOrDefault();
        endPoint = stops.OrderBy(x => x.DistanceToEnd).FirstOrDefault();
        if (startPoint is not null && startPoint.DistanceToStart > _configDistance.Limit) throw new Exception("Nearest stop from start higher than 500m!");
        if (endPoint is not null && endPoint.DistanceToEnd > _configDistance.Limit) throw new Exception("Nearest stop from end higher than 500m!");
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