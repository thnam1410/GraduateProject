using GraduateProject.Application.Common.Dto;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IRouteService
{
    Task<Dictionary<int, AStarPathDto>> GetRoute(FindRouteRequestDto request);
    Task<List<Route>> GetMainRoute();
    Task<object> GetRouteDetailsByRouteId(int routeId);
    Task<object> GetBusStopNearby(Position position);
}