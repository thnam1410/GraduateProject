using GraduateProject.Authentication;
using GraduateProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Migrator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("MY_DB");
            Console.WriteLine("================================");
            Console.WriteLine(connectionString);

            services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    async context => { await context.Response.WriteAsync("Hello World - PVGAS HSEQ!"); });
            });
        }
    }
}