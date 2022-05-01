
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
    private readonly IPathRepository _pathRepository;

    public RouteController(IRouteService routeService, IPathRepository pathRepository)
    {
        _routeService = routeService;
        _pathRepository = pathRepository;
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
        var pathStart = await _pathRepository.Queryable().FirstOrDefaultAsync(x => x.RouteDetailId == 1651 && x.Rank == 1);
        var vertices = await _pathRepository.GetVertexQueryable().Where(x => x.ParentRouteDetailId == routeDetailId)
            .Select(x => new
            {
                x.PointAId, x.PointBId,
                PointALat = x.PointA.Lat, PointALng = x.PointA.Lng,
                PointBLat = x.PointB.Lat, PointBLng = x.PointB.Lng,
            })
            .ToListAsync();
        var firstVertex = vertices.FirstOrDefault(x => x.PointAId == pathStart?.Id);
        var results = new List<Position>();
        if (firstVertex is not null)
        {
            results.AddRange(new List<Position>()
            {
                new() {Lat = firstVertex.PointALat, Lng = firstVertex.PointALng},
                new() {Lat = firstVertex.PointBLat, Lng = firstVertex.PointBLng},
            });
            var currentVertex = firstVertex;
            while (true)
            {
                var nextVertex = vertices.FirstOrDefault(x => x.PointAId == currentVertex.PointBId);
                if (nextVertex is not null)
                {
                    currentVertex = nextVertex;
                    results.Add(new Position() {Lat = nextVertex.PointBLat, Lng = nextVertex.PointBLng});
                }
                else break;
            }
        }

        return ApiResponse<List<Position>>.Ok(results);
    }
}