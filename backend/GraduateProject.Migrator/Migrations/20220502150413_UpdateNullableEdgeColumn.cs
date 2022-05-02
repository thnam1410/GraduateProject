using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateNullableEdgeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Edge",
                table: "Edge");

            migrationBuilder.AlterColumn<int>(
                name: "ParentRouteDetailId",
                table: "Edge",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Edge",
                table: "Edge",
                columns: new[] { "PointAId", "PointBId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Edge",
                table: "Edge");

            migrationBuilder.AlterColumn<int>(
                name: "ParentRouteDetailId",
                table: "Edge",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Edge",
                table: "Edge",
                columns: new[] { "PointAId", "PointBId", "ParentRouteDetailId" });
        }
    }
}
