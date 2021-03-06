using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Constants;
using Microsoft.EntityFrameworkCore;

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
        });
        builder.Entity<InfoRouteSearch>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Route)
                .WithMany()
                .HasForeignKey(x => x.RouteId);
        });
        
        builder.Entity<PathHistory>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
        
        builder.Entity<RouteDetail>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.HasOne(x => x.Route)
                .WithMany(x => x.RouteDetails)
                .HasForeignKey(x => x.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(x => x.Stops)
                .WithMany(x => x.RouteList)
                .UsingEntity<RouteStop>(
                    b => b.HasOne(e => e.Stop).WithMany(e => e.RouteStops).HasForeignKey(e => e.StopId).OnDelete(DeleteBehavior.Cascade),
                    a => a.HasOne(e => e.RouteDetail).WithMany(e => e.RouteStops).HasForeignKey(e => e.RouteDetailId).OnDelete(DeleteBehavior.Cascade)
                );
        });
        
        builder.Entity<Stop>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
        });


        

        builder.Entity<Vertex>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.HasOne(x => x.RouteDetail)
                .WithMany(x => x.Vertices)
                .HasForeignKey(x => x.RouteDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<CrawlPath>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });

        builder.Entity<CrawlRoute>(entity =>
        {
            entity.HasKey(x => x.RouteId);
        });
        
        builder.Entity<CrawlRouteDetail>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
        builder.Entity<CrawlStop>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
        builder.Entity<Edge>(entity =>
        {
            entity.HasKey(x => new {x.PointAId, x.PointBId});

            entity.Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (EdgeType) Enum.Parse(typeof(EdgeType), v))
                .HasMaxLength(30)
                .IsRequired();
            
            entity.HasOne(x => x.PointA)
                .WithMany()
                .HasForeignKey(x => x.PointAId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(x => x.PointB)
                .WithMany()
                .HasForeignKey(x => x.PointBId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ParentRouteDetail)
                .WithMany()
                .HasForeignKey(x => x.ParentRouteDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        builder.Entity<BusStopEdge>(entity =>
        {
            entity.HasKey(x => new {x.PointAId, x.PointBId});
            entity.HasOne(x => x.PointA)
                .WithMany()
                .HasForeignKey(x => x.PointAId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(x => x.PointB)
                .WithMany()
                .HasForeignKey(x => x.PointBId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}