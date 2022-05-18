using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Z.EntityFramework.Plus;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class RouteService : IRouteService
{
    private readonly IRouteDetailRepository _routeDetailRepository;
    private readonly IVertexRepository _vertexRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IOptionsSnapshot<ConfigDistance> _configDistance;
    private readonly IRouteRepository _routeRepository;
    private readonly IObjectMapper _mapper;

    public RouteService(IRouteDetailRepository routeDetailRepository, IVertexRepository vertexRepository, IStopRepository stopRepository,
        IOptionsSnapshot<ConfigDistance> configDistance, IRouteRepository routeRepository, IObjectMapper mapper)
    {
        _routeDetailRepository = routeDetailRepository;
        _vertexRepository = vertexRepository;
        _stopRepository = stopRepository;
        _configDistance = configDistance;
        _routeRepository = routeRepository;
        _mapper = mapper;
    }

    public async Task<object> GetRoute(FindRouteRequestDto request)
    {
        var limitSearchRadius = _configDistance.Value.SearchRadius;
        const int limitTrustNeight = 10;

        var vertices = (await GetVertices()).ToList();
        var edges = await GetEdges();
        var graph = new Graph(vertices, edges);

        var startPointNeighborVertices = new List<VertexDto>();
        var endPointNeighborVertices = new List<VertexDto>();
        foreach (var vertex in vertices)
        {
            var distanceToStart = CalculateUtil.Distance(request.StartPoint, vertex.Position);
            var distanceToEnd = CalculateUtil.Distance(request.EndPoint, vertex.Position);
            if (distanceToStart <= limitSearchRadius)
            {
                vertex.DistanceToStart = distanceToStart;
                vertex.DistanceToEnd = CalculateUtil.Distance(request.EndPoint, vertex.Position);
                startPointNeighborVertices.Add(vertex);
            }

            if (distanceToEnd <= limitSearchRadius)
            {
                vertex.DistanceToEnd = distanceToEnd;
                endPointNeighborVertices.Add(vertex);
            }
        }

        if (!startPointNeighborVertices.Any()) throw new Exception("Route nearby not found!");
        var trustNeighbors = new List<VertexDto>();
        var startPoint = startPointNeighborVertices.OrderBy(x => x.DistanceToStart).First();
        var endPoint = endPointNeighborVertices.OrderBy(x => x.DistanceToEnd).First();

        try
        {
            var aStarCtor = new AStar(graph, startPoint.Id, endPoint.Id);
            var paths = aStarCtor.StartAlgorithms();
            var nodeIds = paths.Select(x => x.Id);
            var positions = paths.Select(x => new Position()
            {
                Lat = x.Lat, Lng = x.Lng
            });
            return new
            {
                positions,
                routes = vertices.Where(x => nodeIds.Contains(x.Id)).GroupBy(x => x.RouteDetailId).Select(x => x.Key)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<IEnumerable<EdgeDto>> GetEdges()
    {
        var edges = await _vertexRepository.GetEdgeQueryable().AsNoTracking()
            .Select(x => new EdgeDto()
            {
                PointAId = x.PointAId,
                PointBId = x.PointBId,
                EdgeDistance = x.Distance,
                Type = x.Type
            }).FromCacheAsync();
        return edges;
    }

    private async Task<IEnumerable<VertexDto>> GetVertices()
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
        return vertices;
    }

    public Task<List<Route>> GetMainRoute()
    {
        return _routeRepository.Queryable().Where(x => x.RouteDetails.Any()).ToListAsync();
    }

    public async Task<object> GetRouteDetailsByRouteId(int routeId)
    {
        var routes = await _routeDetailRepository.Queryable()
            .Where(x => x.RouteId == routeId)
            .Include(x => x.Stops)
            .Include(x=>x.Vertices)
            .Include(x => x.Route)
            .OrderBy(x => x.RouteVarId)
            .ToListAsync();
        if (routes.Any())
        {
            var forwardRoute = routes[0];
            var backwardRoute = routes[1];
            var parentRoute = forwardRoute.Route;
            return new
            {
                routeInfo = new
                {
                    parentRoute.Name, // Ten tuyen
                    parentRoute.RouteCode, // Ma so tuyen
                    parentRoute.Type, //Loại hình hoạt động
                    parentRoute.RouteRange, // Cu ly
                    parentRoute.BusType, // Loai xe
                    parentRoute.TimeRange, //Tgian hoat dong
                    parentRoute.Unit, // Don vi dam nhan
                },
                forwardRouteStops = forwardRoute.Stops.Select(x => new
                {
                    x.Name,
                    position = new Position() {Lat = x.Lat, Lng = x.Lng},
                }), // cac tram dung -> hien marker
                forwardRoutePos = forwardRoute.Vertices.OrderBy(x => x.Rank).Select(x => new Position(){Lat = x.Lat, Lng = x.Lng}),

                backwardRouteStops = backwardRoute.Stops.Select(x => new
                {
                    x.Name,
                    position = new Position() {Lat = x.Lat, Lng = x.Lng},
                }),
                backwardRoutePos = backwardRoute.Vertices.OrderBy(x => x.Rank).Select(x => new Position(){Lat = x.Lat, Lng = x.Lng}),
            };
        }

        throw new Exception();
    }

    public async Task<object> GetBusStopNearby(Position position)
    {
        var stops = await _stopRepository.Queryable().Select(x => new
        {
            x.Lat, x.Lng, x.Name, x.Routes, Address = x.AddressNo, x.Code
        }).FromCacheAsync();
        return stops.Where(x => CalculateUtil.Distance(position, new Position() {Lat = x.Lat, Lng = x.Lng}) <= 1).ToList();

    }
}