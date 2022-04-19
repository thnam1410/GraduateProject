using GraduateProject.Application.RealEstate.MasterDataDto.Services;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Application.Extensions;

public static class RealEstateServiceCollectionExtensions
{
    public static IServiceCollection AddRealEstateServices(this IServiceCollection services)
    {
        services.AddTransient<IMasterDataService, MasterDataService>();
        return services;
    }
}