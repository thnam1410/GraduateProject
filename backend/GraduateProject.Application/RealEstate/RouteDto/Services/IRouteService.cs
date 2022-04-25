namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IRouteService
{
    Task<object> GetRoute(FindRouteRequestDto request);
}