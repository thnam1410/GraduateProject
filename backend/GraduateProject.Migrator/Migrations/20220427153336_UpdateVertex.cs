using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateVertex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vertex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex",
                columns: new[] { "PointAId", "PointBId", "ParentRouteDetailId" });

            migrationBuilder.CreateIndex(
                name: "IX_Vertex_PointBId",
                table: "Vertex",
                column: "PointBId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vertex_Path_PointAId",
                table: "Vertex");

            migrationBuilder.DropForeignKey(
                name: "FK_Vertex_Path_PointBId",
                table: "Vertex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex");

            migrationBuilder.DropIndex(
                name: "IX_Vertex_PointBId",
                table: "Vertex");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Vertex",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vertex",
                table: "Vertex",
                column: "Id");
        }
    }
}
