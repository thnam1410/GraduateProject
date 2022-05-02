
using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.RealEstate.RouteDto;
using GraduateProject.Application.RealEstate.RouteDto.Services;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Controllers;

[Route("/api/route")]
public class RouteController: ControllerBase
{
    private readonly IRouteService _routeService;
    private readonly IVertexRepository _vertexRepository;

    public RouteController(IRouteService routeService, IVertexRepository vertexRepository)
    {
        _routeService = routeService;
        _vertexRepository = vertexRepository;
    }


    [HttpGet("")]
    public async Task<ApiResponse<object>> HandleGetRoutes([FromQuery] FindRouteRequestDto request)
    {
        return ApiResponse<object>.Ok(await _routeService.GetRoute(request));
    }
    
    //Test
    [HttpGet("get-path-by-route-detail-id")]
    public async Task<ApiResponse<List<Position>>> HandleGetRoutePathByRouteDetailId([FromQuery] int routeDetailId)
    {
        var vertexStart = await _vertexRepository.Queryable().FirstOrDefaultAsync(x => x.RouteDetailId == routeDetailId && x.Rank == 1);
        var edges = await _vertexRepository.GetEdgeQueryable().Where(x => x.ParentRouteDetailId == routeDetailId)
            .Select(x => new
            {
                x.PointAId, x.PointBId,
                PointALat = x.PointA.Lat, PointALng = x.PointA.Lng,
                PointBLat = x.PointB.Lat, PointBLng = x.PointB.Lng,
            })
            .ToListAsync();
        var firstEdge = edges.FirstOrDefault(x => x.PointAId == vertexStart?.Id);
        var results = new List<Position>();
        if (firstEdge is not null)
        {
            results.AddRange(new List<Position>()
            {
                new() {Lat = firstEdge.PointALat, Lng = firstEdge.PointALng},
                new() {Lat = firstEdge.PointBLat, Lng = firstEdge.PointBLng},
            });
            var currentEdge = firstEdge;
            while (true)
            {
                var nextVertex = edges.FirstOrDefault(x => x.PointAId == currentEdge.PointBId);
                if (nextVertex is not null)
                {
                    currentEdge = nextVertex;
                    results.Add(new Position() {Lat = nextVertex.PointBLat, Lng = nextVertex.PointBLng});
                }
                else break;
            }
        }

        return ApiResponse<List<Position>>.Ok(results);
    }

    [HttpGet("get-switch-edges-test")]
    public async Task<ApiResponse<object>> HandleGetSwitchEdgeTest()
    {
        var edges = await _vertexRepository.GetEdgeQueryable().Where(x => x.PointAId.ToString() == "E9DC26F8-CD25-4A67-50D4-08DA2B8BFB8C")
            .Select(x => new
            {
                x.PointAId, x.PointBId,
                PointALat = x.PointA.Lat, PointALng = x.PointA.Lng,
                PointBLat = x.PointB.Lat, PointBLng = x.PointB.Lng,
            })
            .ToListAsync();
        return ApiResponse<object>.Ok(edges.Select(x => new Position() {Lng = x.PointBLng, Lat = x.PointBLat}).ToList());
    }
}