using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class ChangeReferenceRouteAndRouteDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Path_Route_RouteId",
                table: "Path");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_Route_RouteId",
                table: "RouteStop");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "EndStop",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "Outbound",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RouteNo",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RouteVarId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RouteVarShortName",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RunningTime",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "StartStop",
                table: "Route");

            migrationBuilder.RenameColumn(
                name: "RouteVarName",
                table: "Route",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "RouteDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteVarId = table.Column<int>(type: "int", nullable: false),
                    RouteVarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    EndStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outbound = table.Column<bool>(type: "bit", nullable: true),
                    RouteVarShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunningTime = table.Column<int>(type: "int", nullable: true),
                    StartStop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteDetail_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteDetail_RouteId",
                table: "RouteDetail",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Path_RouteDetail_RouteId",
                table: "Path",
                column: "RouteId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_RouteDetail_RouteId",
                table: "RouteStop",
                column: "RouteId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Path_RouteDetail_RouteId",
                table: "Path");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_RouteDetail_RouteId",
                table: "RouteStop");

            migrationBuilder.DropTable(
                name: "RouteDetail");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Route",
                newName: "RouteVarName");

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Route",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "EndStop",
                table: "Route",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Outbound",
                table: "Route",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteNo",
                table: "Route",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RouteVarId",
                table: "Route",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RouteVarShortName",
                table: "Route",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RunningTime",
                table: "Route",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartStop",
                table: "Route",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Path_Route_RouteId",
                table: "Path",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_Route_RouteId",
                table: "RouteStop",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
