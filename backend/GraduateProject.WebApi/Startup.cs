using System.Text.Json;
using System.Text.Json.Serialization;
using GraduateProject.Application.Extensions;
using GraduateProject.Application.Ums.Dto;
using GraduateProject.Authentication;
using GraduateProject.Infrastructure.Extensions;
using GraduateProject.Services;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GraduateProject;

public class Startup
{
    private readonly IHostEnvironment _env;
    public static string connectionString { get; private set; }

    public Startup(IConfiguration configuration, IHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
        connectionString = Configuration.GetConnectionString("My_DB");
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .AddValidation(typeof(LoginForm).Assembly)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddControllers();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Graduation Project", Version = "v1"}); });
        services.SetupInfrastructure(connectionString);
        services.RegisterAuthentication(Configuration); // include jwt && identity config
        services.AddScoped<ICurrentUser<Guid>, CurrentUser>();
        services.AddCors(options =>
        {
            options.AddPolicy("FrontendCors",
                builder => builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );
        });
        services.AddServiceCollections();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1");
                c.EnableFilter();
                c.DisplayRequestDuration();
            });
        }

        app.UseCors("FrontendCors");
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}