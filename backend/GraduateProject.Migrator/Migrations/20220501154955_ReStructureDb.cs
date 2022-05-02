using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class ReStructureDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vertex_Path_PointAId",
                table: "Vertex");

            migrationBuilder.DropForeignKey(
                name: "FK_Vertex_Path_PointBId",
                table: "Vertex");

            migrationBuilder.DropForeignKey(
                name: "FK_Vertex_RouteDetail_ParentRouteDetailId",
                table: "Vertex");

            migrationBuilder.DropTable(
                name: "Path");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex");

            migrationBuilder.DropIndex(
                name: "IX_Vertex_PointBId",
                table: "Vertex");

            migrationBuilder.DropColumn(
                name: "PointAId",
                table: "Vertex");

            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "Vertex",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "ParentRouteDetailId",
                table: "Vertex",
                newName: "RouteDetailId");

            migrationBuilder.RenameColumn(
                name: "PointBId",
                table: "Vertex",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Vertex_ParentRouteDetailId",
                table: "Vertex",
                newName: "IX_Vertex_RouteDetailId");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Vertex",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Vertex",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Edge",
                columns: table => new
                {
                    PointAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointBId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentRouteDetailId = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edge", x => new { x.PointAId, x.PointBId, x.ParentRouteDetailId });
                    table.ForeignKey(
                        name: "FK_Edge_RouteDetail_ParentRouteDetailId",
                        column: x => x.ParentRouteDetailId,
                        principalTable: "RouteDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Edge_Vertex_PointAId",
                        column: x => x.PointAId,
                        principalTable: "Vertex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Edge_Vertex_PointBId",
                        column: x => x.PointBId,
                        principalTable: "Vertex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edge_ParentRouteDetailId",
                table: "Edge",
                column: "ParentRouteDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Edge_PointBId",
                table: "Edge",
                column: "PointBId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vertex_RouteDetail_RouteDetailId",
                table: "Vertex",
                column: "RouteDetailId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vertex_RouteDetail_RouteDetailId",
                table: "Vertex");

            migrationBuilder.DropTable(
                name: "Edge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Vertex");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Vertex");

            migrationBuilder.RenameColumn(
                name: "RouteDetailId",
                table: "Vertex",
                newName: "ParentRouteDetailId");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Vertex",
                newName: "Distance");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vertex",
                newName: "PointBId");

            migrationBuilder.RenameIndex(
                name: "IX_Vertex_RouteDetailId",
                table: "Vertex",
                newName: "IX_Vertex_ParentRouteDetailId");

            migrationBuilder.AddColumn<Guid>(
                name: "PointAId",
                table: "Vertex",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex",
                columns: new[] { "PointAId", "PointBId", "ParentRouteDetailId" });

            migrationBuilder.CreateTable(
                name: "Path",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteDetailId = table.Column<int>(type: "int", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Path_RouteDetail_RouteDetailId",
                        column: x => x.RouteDetailId,
                        principalTable: "RouteDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vertex_PointBId",
                table: "Vertex",
                column: "PointBId");

            migrationBuilder.CreateIndex(
                name: "IX_Path_RouteDetailId",
                table: "Path",
                column: "RouteDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vertex_Path_PointAId",
                table: "Vertex",
                column: "PointAId",
                principalTable: "Path",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vertex_Path_PointBId",
                table: "Vertex",
                column: "PointBId",
                principalTable: "Path",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vertex_RouteDetail_ParentRouteDetailId",
                table: "Vertex",
                column: "ParentRouteDetailId",
                principalTable: "RouteDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
