
using GraduateProject.Application.RealEstate.RouteDto;
using GraduateProject.Application.RealEstate.RouteDto.Services;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[Route("/api/route")]
public class RouteController: ControllerBase
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService routeService)
    {
        _routeService = routeService;
    }


    [HttpGet("")]
    public async Task<ApiResponse<object>> HandleGetRoutes([FromQuery] FindRouteRequestDto request)
    {
        return ApiResponse<object>.Ok(await _routeService.GetRoute(request));
    }
}