using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class CreateAppEntites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RealEstate");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                schema: "Ums",
                table: "UserAccount",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarAttachFileId",
                schema: "Ums",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                schema: "Ums",
                table: "UserAccount",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterData",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MasterKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterData_MasterData_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "RealEstate",
                        principalTable: "MasterData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferPackage",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActiveDay = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproveStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "False"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalePersonInfo",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phonenumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phonenumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePersonInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpHistory",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUpHistory_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "Ums",
                        principalTable: "UserAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalePersonInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PostStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUseFreeDayConfig = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PriorityOrderRank = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_OfferPackage_OfferPackageId",
                        column: x => x.OfferPackageId,
                        principalSchema: "RealEstate",
                        principalTable: "OfferPackage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_SalePersonInfo_SalePersonInfoId",
                        column: x => x.SalePersonInfoId,
                        principalSchema: "RealEstate",
                        principalTable: "SalePersonInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "Ums",
                        principalTable: "UserAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RealEstate",
                schema: "RealEstate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    BedroomCount = table.Column<int>(type: "int", nullable: false),
                    ToiletCount = table.Column<int>(type: "int", nullable: false),
                    FloorCount = table.Column<int>(type: "int", nullable: false),
                    IsFrontLine = table.Column<bool>(type: "bit", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarAttachFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PicturesAttachFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    YoutubeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapLocationJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstate_MasterData_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "RealEstate",
                        principalTable: "MasterData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstate_MasterData_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "RealEstate",
                        principalTable: "MasterData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstate_MasterData_PriceTypeId",
                        column: x => x.PriceTypeId,
                        principalSchema: "RealEstate",
                        principalTable: "MasterData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstate_MasterData_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalSchema: "RealEstate",
                        principalTable: "MasterData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstate_MasterData_WardId",
                        column: x => x.WardId,
                        principalSchema: "RealEstate",
                        principalTable: "MasterData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstate_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "RealEstate",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstate_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "RealEstate",
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_ParentId",
                schema: "RealEstate",
                table: "MasterData",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_OfferPackageId",
                schema: "RealEstate",
                table: "Post",
                column: "OfferPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_SalePersonInfoId",
                schema: "RealEstate",
                table: "Post",
                column: "SalePersonInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserAccountId",
                schema: "RealEstate",
                table: "Post",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_CountryId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_DistrictId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_PostId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_PriceTypeId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "PriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_ProjectId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_ServiceTypeId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstate_WardId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUpHistory_UserAccountId",
                schema: "RealEstate",
                table: "TopUpHistory",
                column: "UserAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstate",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "TopUpHistory",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "MasterData",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "OfferPackage",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "SalePersonInfo",
                schema: "RealEstate");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Ums",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "AvatarAttachFileId",
                schema: "Ums",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "DOB",
                schema: "Ums",
                table: "UserAccount");
        }
    }
}
