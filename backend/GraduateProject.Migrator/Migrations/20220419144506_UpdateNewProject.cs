using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateNewProject : Migration
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
                name: "FileEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntry", x => x.Id);
                });

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
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteVarId = table.Column<int>(type: "int", nullable: false),
                    RouteVarName = table.Column<int>(type: "int", nullable: false),
                    RouteNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    EndStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outbound = table.Column<bool>(type: "bit", nullable: true),
                    RouteVarShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunningTime = table.Column<int>(type: "int", nullable: true),
                    StartStop = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Routes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StopType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Path_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteStop",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    StopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStop", x => new { x.RouteId, x.StopId });
                    table.ForeignKey(
                        name: "FK_RouteStop_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStop_Stop_StopId",
                        column: x => x.StopId,
                        principalTable: "Stop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_ParentId",
                schema: "RealEstate",
                table: "MasterData",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Path_RouteId",
                table: "Path",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStop_StopId",
                table: "RouteStop",
                column: "StopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileEntry");

            migrationBuilder.DropTable(
                name: "MasterData",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "Path");

            migrationBuilder.DropTable(
                name: "RouteStop");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Stop");

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
