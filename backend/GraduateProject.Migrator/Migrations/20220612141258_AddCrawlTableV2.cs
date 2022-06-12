using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class AddCrawlTableV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrawlRoute",
                columns: table => new
                {
                    Distance = table.Column<long>(type: "bigint", nullable: true),
                    Headway = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InBoundDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InBoundName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfSeats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orgs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutBoundDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutBoundName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tickets = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOfTrip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTrip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrawlRoute");
        }
    }
}
