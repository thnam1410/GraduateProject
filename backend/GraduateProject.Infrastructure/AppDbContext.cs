using GraduateProject.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureUmsEntities();
    }
}