using GraduateProject.Domain.AppEntities.Repositories;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IFindRouteService
{
    Task<object> GetRoute(FindRouteRequestDto request);

}