using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.RealEstate.RouteDto;
using GraduateProject.Application.RealEstate.RouteDto.Services;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace GraduateProject.Controllers;

[Route("/api/route")]
public class RouteController : ControllerBase
{
    private readonly IRouteService _routeService;
    private readonly IVertexRepository _vertexRepository;
    private readonly IFindRouteService _findRouteService;

    public RouteController(IRouteService routeService, IVertexRepository vertexRepository, IFindRouteService findRouteService)
    {
        _routeService = routeService;
        _vertexRepository = vertexRepository;
        _findRouteService = findRouteService;
    }


    [HttpPost("")]
    public async Task<ApiResponse<RouteResponseDto>> HandleGetRoutes([FromBody] FindRouteRequestDto request)
    {
        try
        {
            return ApiResponse<RouteResponseDto>.Ok(await _routeService.GetRoute(request));
        }
        catch (Exception e)
        {
            return ApiResponse<RouteResponseDto>.Fail(e.Message);
        }
    }    
    
    [HttpPost("test")]
    public async Task<ApiResponse<object>> HandleGetRoutesTest([FromBody] FindRouteRequestDto request)
    {
        try
        {
            return ApiResponse<object>.Ok(await _findRouteService.GetRoute(request));
        }
        catch (Exception e)
        {
            return ApiResponse<object>.Fail(e.Message);
        }
    }

    [HttpPost("create-info-route-search")]
    public async Task<ApiResponse> HandleCreateRouteSearch([FromBody] InfoRouteSearchDto request)
    {
        await _routeService.CreateInfoRouteSearch(request);
        return ApiResponse.Ok();

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

    [HttpGet("get-info-search")]
    public async Task<ApiResponse<List<InfoRouteSearchViewDto>>> HandleGetInfoSearchByUser()
    {
        var result = await _routeService.GetInfoRouteSearch("a");
        return ApiResponse<List<InfoRouteSearchViewDto>>.Ok(result);
    }
    [HttpGet("get-main-routes")]
    public async Task<ApiResponse<List<Domain.AppEntities.Entities.Route>>> HandleGetMainRoutes()
    {
        return ApiResponse<List<Domain.AppEntities.Entities.Route>>.Ok(await _routeService.GetMainRoute());
    }

    [HttpGet("get-route-info/{routeId}")]
    public async Task<ApiResponse<object>> HandleGetRouteInfoById([FromRoute] int routeId)
    {
        return ApiResponse<object>.Ok(await _routeService.GetRouteDetailsByRouteId(routeId));
    }
    
    [HttpGet("get-route-info-search/{userId}")]
    public async Task<ApiResponse<object>> HandleGetRouteInfoSearchByUser([FromRoute] string userId)
    {
        return ApiResponse<object>.Ok(await _routeService.GetInfoRouteSearch(userId));
    }

    [HttpGet("get-bus-stop-nearby")]
    public async Task<ApiResponse<object>> HandleGetBusStopNearby([FromQuery] Position position)
    {
        return ApiResponse<object>.Ok(await _routeService.GetBusStopNearby(position));
    }
}