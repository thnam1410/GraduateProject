using AutoMapper;
using AutoMapper.QueryableExtensions;
using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Z.EntityFramework.Plus;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class FindRouteService: IFindRouteService
{
    private readonly IVertexRepository _vertexRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IOptionsSnapshot<ConfigDistance> _configDistance;
    private readonly IMapper _mapper;
    private readonly IAStarService _aStarService;
    private readonly ILogger<FindRouteService> _logger;


    public FindRouteService(IVertexRepository vertexRepository, IOptionsSnapshot<ConfigDistance> configDistance, IStopRepository stopRepository, IMapper mapper, IAStarService aStarService, ILogger<FindRouteService> logger)
    {
        _vertexRepository = vertexRepository;
        _configDistance = configDistance;
        _stopRepository = stopRepository;
        _mapper = mapper;
        _aStarService = aStarService;
        _logger = logger;
    }

    public async Task<object> GetRoute(FindRouteRequestDto request)
    {
        var limitSearchRadius = _configDistance.Value.SearchRadius;
        var stops = await _stopRepository.Queryable().ProjectTo<StopDto>(_mapper.ConfigurationProvider).ToListAsync();
        var stopPaths = await GetBusStopList(request, stops, limitSearchRadius);
        var results = new List<Position>();
        await GetRoutePathList(stopPaths, limitSearchRadius, results);

        results.Insert(0, request.StartPoint);
        results.Add(request.EndPoint);
        return new
        {
            results,
            stops = stops.Where(x => stopPaths.Select(y => y.Id).Contains(x.Id))
        };
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

    private async Task<List<AStarNodeBusStop>> GetBusStopList(FindRouteRequestDto request, List<StopDto> stops, double limitSearchRadius)
    {
        var startPointNeighborVertices = new List<StopDto>();
        var endPointNeighborVertices = new List<StopDto>();
        foreach (var stop in stops)
        {
            var distanceToStart = CalculateUtil.Distance(request.StartPoint, stop.Position);
            var distanceToEnd = CalculateUtil.Distance(request.EndPoint, stop.Position);
            if (distanceToStart <= limitSearchRadius)
            {
                stop.DistanceToStart = distanceToStart;
                stop.DistanceToEnd = CalculateUtil.Distance(request.EndPoint, stop.Position);
                startPointNeighborVertices.Add(stop);
            }

            if (distanceToEnd <= limitSearchRadius)
            {
                stop.DistanceToEnd = distanceToEnd;
                endPointNeighborVertices.Add(stop);
            }
        }

        if (!startPointNeighborVertices.Any()) throw new Exception("Stop nearby not found!");
        var startPoint = startPointNeighborVertices.OrderBy(x => x.DistanceToStart).First();
        var endPoint = endPointNeighborVertices.OrderBy(x => x.DistanceToEnd).First();

        var stopPaths = await _aStarService.StartAlgorithmsBusStopPaths(startPoint, endPoint, stops);
        return stopPaths;
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