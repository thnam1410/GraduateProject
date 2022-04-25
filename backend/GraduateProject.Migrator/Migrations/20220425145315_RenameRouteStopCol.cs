using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class RenameRouteStopCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_RouteDetail_RouteId",
                table: "RouteStop");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "RouteStop",
                newName: "RouteDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_RouteDetail_RouteDetailId",
                table: "RouteStop",
                column: "RouteDetailId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_RouteDetail_RouteDetailId",
                table: "RouteStop");

            migrationBuilder.RenameColumn(
                name: "RouteDetailId",
                table: "RouteStop",
                newName: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_RouteDetail_RouteId",
                table: "RouteStop",
                column: "RouteId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
