using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Path = GraduateProject.Domain.AppEntities.Entities.Path;

namespace GraduateProject.Infrastructure.EntityConfigurations;

public static class AppEntitiesConfiguration
{
    public static string AppEntitiesSchema = "RealEstate";

    public static void ConfigAppEntities(this ModelBuilder builder)
    {
        builder.Entity<MasterData>(entity =>
        {
            entity.ToTable(nameof(MasterData), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.HasOne(x => x.ParentMasterData)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<FileEntry>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });

        builder.Entity<Route>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.HasMany(x => x.Stops)
                .WithMany(x => x.RouteList)
                .UsingEntity<RouteStop>(
                    b => b.HasOne(e => e.Stop).WithMany(e => e.RouteStops).HasForeignKey(e => e.StopId).OnDelete(DeleteBehavior.Cascade),
                    a => a.HasOne(e => e.Route).WithMany(e => e.RouteStops).HasForeignKey(e => e.RouteId).OnDelete(DeleteBehavior.Cascade)
                );
        });
        builder.Entity<Stop>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
        });
        builder.Entity<Path>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.HasOne(x => x.Route)
                .WithMany(x => x.Paths)
                .HasForeignKey(x => x.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<CrawlPath>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
        builder.Entity<CrawlRoute>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
        builder.Entity<CrawlStop>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
    }
}