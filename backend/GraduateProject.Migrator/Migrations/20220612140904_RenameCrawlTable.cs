using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class RenameCrawlTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrawlRoute");

            migrationBuilder.RenameColumn(
                name: "StopID",
                table: "CrawlStop",
                newName: "StopId");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StopType",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "CrawlStop",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Search",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Routes",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Lng",
                table: "CrawlStop",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "CrawlStop",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AddressNo",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zone",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CrawlRouteDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Distance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndStop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outbound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RunningTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartStop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlRouteDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoRouteSearch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    IsSearch = table.Column<bool>(type: "bit", nullable: true),
                    DepartPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSearch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoRouteSearch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoRouteSearch_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoRouteSearch_RouteId",
                table: "InfoRouteSearch",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrawlRouteDetail");

            migrationBuilder.DropTable(
                name: "InfoRouteSearch");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "CrawlStop");

            migrationBuilder.DropColumn(
                name: "Zone",
                table: "CrawlStop");

            migrationBuilder.RenameColumn(
                name: "StopId",
                table: "CrawlStop",
                newName: "StopID");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StopType",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StopID",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Search",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Routes",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lng",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Lat",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressNo",
                table: "CrawlStop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CrawlRoute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Distance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndStop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outbound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteVarShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RunningTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartStop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlRoute", x => x.Id);
                });
        }
    }
}
