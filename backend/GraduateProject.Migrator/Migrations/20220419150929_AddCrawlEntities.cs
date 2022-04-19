using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class AddCrawlEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrawlPath",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlPath", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrawlRoute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Distance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndStop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outbound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RunningTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartStop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlRoute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrawlStop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    AddressNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Routes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StopID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StopType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlStop", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrawlPath");

            migrationBuilder.DropTable(
                name: "CrawlRoute");

            migrationBuilder.DropTable(
                name: "CrawlStop");
        }
    }
}
