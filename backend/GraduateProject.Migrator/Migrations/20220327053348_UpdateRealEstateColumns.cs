using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduateProject.Migrator.Migrations
{
    public partial class UpdateRealEstateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_OfferPackage_OfferPackageId",
                schema: "RealEstate",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_MasterData_CountryId",
                schema: "RealEstate",
                table: "RealEstate");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                schema: "RealEstate",
                table: "RealEstate",
                newName: "ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_RealEstate_CountryId",
                schema: "RealEstate",
                table: "RealEstate",
                newName: "IX_RealEstate_ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_OfferPackage_OfferPackageId",
                schema: "RealEstate",
                table: "Post",
                column: "OfferPackageId",
                principalSchema: "RealEstate",
                principalTable: "OfferPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_MasterData_ProvinceId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "ProvinceId",
                principalSchema: "RealEstate",
                principalTable: "MasterData",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_OfferPackage_OfferPackageId",
                schema: "RealEstate",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_MasterData_ProvinceId",
                schema: "RealEstate",
                table: "RealEstate");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                schema: "RealEstate",
                table: "RealEstate",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_RealEstate_ProvinceId",
                schema: "RealEstate",
                table: "RealEstate",
                newName: "IX_RealEstate_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_OfferPackage_OfferPackageId",
                schema: "RealEstate",
                table: "Post",
                column: "OfferPackageId",
                principalSchema: "RealEstate",
                principalTable: "OfferPackage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_MasterData_CountryId",
                schema: "RealEstate",
                table: "RealEstate",
                column: "CountryId",
                principalSchema: "RealEstate",
                principalTable: "MasterData",
                principalColumn: "Id");
        }
    }
}
