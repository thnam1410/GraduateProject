using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class RouteService: IRouteService
{
    private readonly IRouteDetailRepository _routeDetailRepository;
    private readonly IVertexRepository _vertexRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IOptionsSnapshot<ConfigDistance> _configDistance;
    private readonly IRouteRepository _routeRepository;
    private readonly IObjectMapper _mapper;

    public RouteService(IRouteDetailRepository routeDetailRepository, IVertexRepository vertexRepository, IStopRepository stopRepository, IOptionsSnapshot<ConfigDistance> configDistance, IRouteRepository routeRepository, IObjectMapper mapper)
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
        var searchRadius = _configDistance.Value.SearchRadius;
        var vertices = await _vertexRepository.Queryable().AsNoTracking()
            .Select(x => new VertexDto()
            {
                Id = x.Id,
                Lat = x.Lat,
                Lng = x.Lng,
                Rank = x.Rank,
                Status = VertexStatus.Temporary,
                MinCost = double.MaxValue
            })
            .AsSplitQuery()
            .ToListAsync();
        var vertexIds = vertices.Select(x => x.Id).ToList();
        var edges = await _vertexRepository.GetEdgeQueryable().AsNoTracking()
            // .Where(x => vertexIds.Contains(x.PointAId) || vertexIds.Contains(x.PointBId))
            .Select(x => new EdgeDto()
            {
                PointAId = x.PointAId,
                PointBId = x.PointBId,
                EdgeDistance = x.Distance,
                Type = x.Type
            }).AsSplitQuery().ToListAsync();
        var graph = new Graph(vertices, edges);
        try
        {
            var paths = new AStar(graph, new Guid("96688B36-6FDE-4B49-B860-08DA2B8BFB8B"), new Guid("FB600B07-CD0F-452C-36D0-08DA2B8BFB8C"));
            return paths.StartAlgorithms().Select(x => new Position()
            {
                Lat = x.Lat, Lng = x.Lng
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Route>> GetMainRoute()
    {
        return _routeRepository.Queryable().Where(x => x.RouteDetails.Any()).ToListAsync();
    }

    public async Task<object> GetRouteDetailsByRouteId(int routeId)
    {
        var routes = await _routeDetailRepository.Queryable().Where(x => x.RouteId == routeId)
            .Include(x => x.Stops)
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
                    parentRoute.Name,  // Ten tuyen
                    parentRoute.RouteCode,  // Ma so tuyen
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
                }),

                backwardRouteStops = backwardRoute.Stops.Select(x => new
                {
                    x.Name,
                    position = new Position() {Lat = x.Lat, Lng = x.Lng},
                }),
            };
        }
        throw new Exception();
    }
}