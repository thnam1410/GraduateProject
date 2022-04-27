using GraduateProject.Application.Common.Dto;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.Extensions.Options;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class RouteService: IRouteService
{
    private readonly IRouteDetailRepository _routeDetailRepository;
    private readonly IPathRepository _pathRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IOptionsSnapshot<ConfigDistance> _configDistance;

    public RouteService(IRouteDetailRepository routeDetailRepository, IPathRepository pathRepository, IStopRepository stopRepository, IOptionsSnapshot<ConfigDistance> configDistance)
    {
        _routeDetailRepository = routeDetailRepository;
        _pathRepository = pathRepository;
        _stopRepository = stopRepository;
        _configDistance = configDistance;
    }

    public Task<object> GetRoute(FindRouteRequestDto request)
    {
        var searchRadius = _configDistance.Value.SearchRadius;
        throw new NotImplementedException();
    }
}