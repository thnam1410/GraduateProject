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
        builder.Entity<OfferPackage>(entity =>
        {
            entity.ToTable(nameof(OfferPackage), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Active).HasDefaultValue(false);
        });
        builder.Entity<Post>(entity =>
        {
            entity.ToTable(nameof(Post), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Active).HasDefaultValue(false);
            entity.Property(x => x.IsUseFreeDayConfig).HasDefaultValue(false);
            entity.HasOne(x => x.UserAccount)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.OfferPackage)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.OfferPackageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.SalePersonInfo)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.SalePersonInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
        builder.Entity<Project>(entity =>
        {
            entity.ToTable(nameof(Project), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Active).HasDefaultValue(false);
            entity.Property(x => x.ApproveStatus)
                .HasConversion(x => x.ToString(), x => (ApproveStatus) Enum.Parse(typeof(ApproveStatus), x));
        });
        builder.Entity<RealEstate>(entity =>
        {
            entity.ToTable(nameof(RealEstate), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.Property(x => x.Direction)
                .HasConversion(x => x.ToString(), x => (HomeDirection) Enum.Parse(typeof(HomeDirection), x));
            entity.HasOne(x => x.Project).WithMany(x => x.RealEstates).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.District).WithMany().HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.Ward).WithMany().HasForeignKey(x => x.WardId).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.PriceType).WithMany().HasForeignKey(x => x.PriceTypeId).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(x => x.ServiceType).WithMany().HasForeignKey(x => x.ServiceTypeId).OnDelete(DeleteBehavior.ClientSetNull);
        });
        builder.Entity<SalePersonInfo>(entity =>
        {
            entity.ToTable(nameof(SalePersonInfo), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        });
        builder.Entity<TopUpHistory>(entity =>
        {
            entity.ToTable(nameof(TopUpHistory), AppEntitiesSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.HasOne(x => x.UserAccount).WithMany().HasForeignKey(x => x.UserAccountId).OnDelete(DeleteBehavior.ClientCascade);
        });
    }
}