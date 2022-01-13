using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollections(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IObjectMapper, BaseObjectMapper>();
        return services;
    }
}