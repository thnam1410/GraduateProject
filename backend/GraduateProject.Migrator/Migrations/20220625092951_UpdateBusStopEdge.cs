using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateBusStopEdge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusStopEdge",
                columns: table => new
                {
                    PointAId = table.Column<int>(type: "int", nullable: false),
                    PointBId = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStopEdge", x => new { x.PointAId, x.PointBId });
                    table.ForeignKey(
                        name: "FK_BusStopEdge_Stop_PointAId",
                        column: x => x.PointAId,
                        principalTable: "Stop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusStopEdge_Stop_PointBId",
                        column: x => x.PointBId,
                        principalTable: "Stop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusStopEdge_PointBId",
                table: "BusStopEdge",
                column: "PointBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusStopEdge");
        }
    }
}
