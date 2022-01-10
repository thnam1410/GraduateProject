using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GraduateProject.Authentication;

public static class JwtTokenServiceExtensions
{
    public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Jwt Setings
        var jwtSettings = new JwtSettings();
        configuration.Bind("JsonWebTokenKeys", jwtSettings);
        services.AddSingleton(jwtSettings);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidAudience = jwtSettings.ValidAudience,
                RequireExpirationTime = jwtSettings.RequireExpirationTime,
                ValidateLifetime = jwtSettings.RequireExpirationTime,
                ClockSkew = TimeSpan.FromDays(1),
            };
        });
    }
}

public class JwtSettings
{
    public bool ValidateIssuerSigningKey { get; set; }
    public string IssuerSigningKey { get; set; }
    public bool ValidateIssuer { get; set; } = true;
    public string ValidIssuer { get; set; }
    public bool ValidateAudience { get; set; } = true;
    public string ValidAudience { get; set; }
    public bool RequireExpirationTime { get; set; }
    public bool ValidateLifetime { get; set; } = true;
}