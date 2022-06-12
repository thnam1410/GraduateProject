using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateCrawlTableV2_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "CrawlRoute",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Distance",
                table: "CrawlRoute",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
