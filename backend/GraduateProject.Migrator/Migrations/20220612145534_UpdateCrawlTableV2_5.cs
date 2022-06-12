using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateCrawlTableV2_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CrawlRoute");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "CrawlRoute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CrawlRoute",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "CrawlRoute",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
