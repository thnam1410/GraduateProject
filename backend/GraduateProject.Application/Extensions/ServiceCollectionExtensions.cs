using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollections(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IObjectMapper, BaseObjectMapper>();
        services.AddRealEstateServices();
        return services;
    }
    
    public static IMvcBuilder AddValidation(
        this IMvcBuilder builder,
        params Assembly[] assemblies)
    {
        builder.Services.AddTransient<IValidator, BaseValidator>();
        builder.AddFluentValidation((Action<FluentValidationMvcConfiguration>) (fv => fv.RegisterValidatorsFromAssemblies((IEnumerable<Assembly>) assemblies)));
        return builder;
    }
}