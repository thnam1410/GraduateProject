using GraduateProject.Application.Common.Dto;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IRouteService
{
    Task<RouteResponseDto> GetRoute(FindRouteRequestDto request);
    Task<List<Route>> GetMainRoute();
    Task<object> GetRouteDetailsByRouteId(int routeId);
    Task<object> GetBusStopNearby(Position position);
    Task<List<InfoRouteSearchViewDto>> GetInfoRouteSearch(string userId);
    Task CreateInfoRouteSearch(InfoRouteSearchDto request);
}