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
        return services;
    }
}