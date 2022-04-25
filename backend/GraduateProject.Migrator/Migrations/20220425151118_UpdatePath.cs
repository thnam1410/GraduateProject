using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdatePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Path_RouteDetail_RouteId",
                table: "Path");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Path",
                newName: "RouteDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Path_RouteId",
                table: "Path",
                newName: "IX_Path_RouteDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Path_RouteDetail_RouteDetailId",
                table: "Path",
                column: "RouteDetailId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Path_RouteDetail_RouteDetailId",
                table: "Path");

            migrationBuilder.RenameColumn(
                name: "RouteDetailId",
                table: "Path",
                newName: "RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Path_RouteDetailId",
                table: "Path",
                newName: "IX_Path_RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Path_RouteDetail_RouteId",
                table: "Path",
                column: "RouteId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
