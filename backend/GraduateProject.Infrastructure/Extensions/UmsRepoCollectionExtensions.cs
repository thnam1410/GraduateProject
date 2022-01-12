using GraduateProject.Domain.Ums.Repositories;
using GraduateProject.Infrastructure.Repositories.Ums;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Infrastructure.Extensions;

public static class UmsRepoCollectionExtensions
{
    public static IServiceCollection AddUmsRepoCollection(this IServiceCollection services)
    {
        services.AddTransient<IUserAccountRepository, UserAccountRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        return services;
    }
}