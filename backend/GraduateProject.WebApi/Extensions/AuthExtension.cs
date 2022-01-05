namespace GraduateProject.Extensions;

public static class AuthExtension
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication();
    }
}