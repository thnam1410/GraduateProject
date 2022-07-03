using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateStopEntityWithRouteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Stop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Stop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "CrawlStop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "CrawlStop",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Stop");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Stop");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "CrawlStop");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "CrawlStop");
        }
    }
}
