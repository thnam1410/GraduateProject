using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Infrastructure.Repositories.Bus;
using GraduateProject.Infrastructure.Repositories.RealEstate;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Infrastructure.Extensions;

public static class RepositoryCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IMasterDataRepository, MasterDataRepository>();
        services.AddTransient<ICrawlEntityRepository, CrawlEntityRepository>();
        services.AddTransient<IRouteDetailRepository, RouteDetailRepository>();
        services.AddTransient<IVertexRepository, VertexRepository>();
        services.AddTransient<IStopRepository, StopRepository>();
        services.AddTransient<IRouteRepository, RouteRepository>();
        services.AddTransient<IPathHistoryRepository, PathHistoryRepository>();
        return services;
    }
}