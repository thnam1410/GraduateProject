using GraduateProject.Domain.AppEntities.Repositories;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IFindRouteService
{
    Task<RouteResponseDtoV2> GetRoute(FindRouteRequestDto request);

}