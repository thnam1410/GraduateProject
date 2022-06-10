using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Ums.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Authentication;

public class IdentityContext : IdentityDbContext<UserAccount, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public static string UmsSchemaName = "Ums";

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigIdentityContext(builder);
    }

    private void ConfigIdentityContext(ModelBuilder builder)
    {
        builder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccount", UmsSchemaName);
            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.HasMany<UserRole>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        });
        builder.Entity<Role>(entity =>
        {
            entity.ToTable("Role", UmsSchemaName);
            entity.HasKey(r => r.Id);

            entity.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            entity.HasIndex(r => r.NormalizedName).IsUnique();
            entity.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            entity.Property(u => u.Name).HasMaxLength(256);
            entity.Property(u => u.NormalizedName).HasMaxLength(256);
            entity.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            entity.HasMany<RoleClaim>()
                .WithOne()
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });
        builder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("RoleClaim", UmsSchemaName);
            entity.HasKey(rc => rc.Id);
        });

        builder.Entity<UserClaim>(entity =>
        {
            entity.ToTable("UserClaim", UmsSchemaName);
            entity.HasKey(rc => rc.Id);
        });

        builder.Entity<UserToken>(entity =>
        {
            entity.ToTable("UserToken", UmsSchemaName);
            entity.HasKey(t => new {t.UserId, t.LoginProvider, t.Name});
        });

        builder.Entity<UserLogin>(entity =>
        {
            entity.ToTable("UserLogin", UmsSchemaName);
            entity.HasKey(t => new {t.UserId, t.LoginProvider, t.ProviderKey});
        });
        builder.Entity<InfoRouteSearch>(entity =>
        {
            entity.ToTable("InfoRouteSearch", UmsSchemaName);
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Route)
                .WithMany()
                .HasForeignKey(x => x.RouteId);
        });
        builder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole", UmsSchemaName);
            entity.HasKey(e => new {e.RoleId, e.UserId});

            entity.HasOne(d => d.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UserAccount)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}