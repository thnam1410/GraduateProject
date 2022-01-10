using GraduateProject.Authentication;
using GraduateProject.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

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
        services.AddControllers();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Graduation Project", Version = "v1"}); });
        services.RegisterAuthentication(Configuration); // include jwt && identity config
        services.SetupInfrastructure(connectionString);
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

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}