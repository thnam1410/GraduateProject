using GraduateProject.Application.RealEstate.MasterDataDto.Services;
using GraduateProject.Application.RealEstate.RouteDto.Services;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Application.Extensions;

public static class RealEstateServiceCollectionExtensions
{
    public static IServiceCollection AddRealEstateServices(this IServiceCollection services)
    {
        services.AddTransient<IMasterDataService, MasterDataService>();
        services.AddTransient<IRouteService, RouteService>();
        services.AddTransient<IFindRouteService, FindRouteService>();
        services.AddTransient<IAStarService, AStarService>();
        return services;
    }
}