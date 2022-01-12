using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddRepositories();
        services.AddUmsRepoCollection();
        
        //Unit Of Work
        services.AddTransient<IUnitOfWork, AppUnitOfWork>();
        return services;
    }
}