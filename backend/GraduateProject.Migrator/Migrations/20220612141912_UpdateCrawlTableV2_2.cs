using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateCrawlTableV2_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Distance",
                table: "CrawlRoute",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Distance",
                table: "CrawlRoute",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
