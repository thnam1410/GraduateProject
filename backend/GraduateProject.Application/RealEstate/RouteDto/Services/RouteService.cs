using GraduateProject.Application.Common.Dto;
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

    public RouteService(IRouteDetailRepository routeDetailRepository, IVertexRepository vertexRepository, IStopRepository stopRepository, IOptionsSnapshot<ConfigDistance> configDistance)
    {
        _routeDetailRepository = routeDetailRepository;
        _vertexRepository = vertexRepository;
        _stopRepository = stopRepository;
        _configDistance = configDistance;
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
            var dijkstra = new Dijkstra(graph, new Guid("96688B36-6FDE-4B49-B860-08DA2B8BFB8B"));
            dijkstra.DoDijkstra();
            dijkstra.GeneratePathFromDestId(new Guid("697D8947-3F2C-4EB8-FCD2-08DA2B8BFB8B"));
            var paths = dijkstra.GetPaths();
            return paths.Select(x => new Position()
            {
                Lat = x.Lat,
                Lng = x.Lng,
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}