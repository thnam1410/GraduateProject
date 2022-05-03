using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IRouteService
{
    Task<object> GetRoute(FindRouteRequestDto request);
    Task<List<Route>> GetMainRoute();
    Task<object> GetRouteDetailsByRouteId(int routeId);
}