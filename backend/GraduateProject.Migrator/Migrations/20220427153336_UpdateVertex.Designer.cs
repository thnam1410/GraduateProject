﻿// <auto-generated />
using System;
using GraduateProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220427153336_UpdateVertex")]
    partial class UpdateVertex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.CrawlPath", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("RouteVarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CrawlPath");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.CrawlRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Distance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndStop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Outbound")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteVarId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteVarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteVarShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RunningTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartStop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CrawlRoute");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.CrawlStop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("AddressNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Routes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Search")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StopID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StopType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CrawlStop");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.FileEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FileLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FileEntry");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.MasterData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MasterKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("MasterData", "RealEstate");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Path", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("RouteDetailId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteDetailId");

                    b.ToTable("Path");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.RouteDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("EndStop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Outbound")
                        .HasColumnType("bit");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<string>("RouteNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RouteVarId")
                        .HasColumnType("int");

                    b.Property<string>("RouteVarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteVarShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RunningTime")
                        .HasColumnType("int");

                    b.Property<string>("StartStop")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteDetail");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.RouteStop", b =>
                {
                    b.Property<int>("RouteDetailId")
                        .HasColumnType("int");

                    b.Property<int>("StopId")
                        .HasColumnType("int");

                    b.HasKey("RouteDetailId", "StopId");

                    b.HasIndex("StopId");

                    b.ToTable("RouteStop");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Routes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Search")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StopType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stop");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Vertex", b =>
                {
                    b.Property<Guid>("PointAId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PointBId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ParentRouteDetailId")
                        .HasColumnType("int");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.HasKey("PointAId", "PointBId", "ParentRouteDetailId");

                    b.HasIndex("ParentRouteDetailId");

                    b.HasIndex("PointBId");

                    b.ToTable("Vertex");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("RoleClaim", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<long?>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("AvatarAttachFileId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAccount", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserClaim", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserLogin", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "ProviderKey");

                    b.ToTable("UserLogin", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", "Ums");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.MasterData", b =>
                {
                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.MasterData", "ParentMasterData")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentMasterData");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Path", b =>
                {
                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.RouteDetail", "RouteDetail")
                        .WithMany("Paths")
                        .HasForeignKey("RouteDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RouteDetail");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.RouteDetail", b =>
                {
                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.Route", "Route")
                        .WithMany("RouteDetails")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.RouteStop", b =>
                {
                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.RouteDetail", "RouteDetail")
                        .WithMany("RouteStops")
                        .HasForeignKey("RouteDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.Stop", "Stop")
                        .WithMany("RouteStops")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RouteDetail");

                    b.Navigation("Stop");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Vertex", b =>
                {
                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.RouteDetail", "ParentRouteDetail")
                        .WithMany()
                        .HasForeignKey("ParentRouteDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.Path", "PointA")
                        .WithMany()
                        .HasForeignKey("PointAId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduateProject.Domain.AppEntities.Entities.Path", "PointB")
                        .WithMany()
                        .HasForeignKey("PointBId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentRouteDetail");

                    b.Navigation("PointA");

                    b.Navigation("PointB");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.RoleClaim", b =>
                {
                    b.HasOne("GraduateProject.Domain.Ums.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraduateProject.Domain.Ums.Entities.Role", null)
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId1");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserRole", b =>
                {
                    b.HasOne("GraduateProject.Domain.Ums.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .IsRequired();

                    b.HasOne("GraduateProject.Domain.Ums.Entities.UserAccount", "UserAccount")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.MasterData", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Route", b =>
                {
                    b.Navigation("RouteDetails");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.RouteDetail", b =>
                {
                    b.Navigation("Paths");

                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("GraduateProject.Domain.AppEntities.Entities.Stop", b =>
                {
                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.Role", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("GraduateProject.Domain.Ums.Entities.UserAccount", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
