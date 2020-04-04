using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class Parsel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Parcels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Parcels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CompanyId",
                table: "Parcels",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Companies_CompanyId",
                table: "Parcels",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Companies_CompanyId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_CompanyId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Parcels");
        }
    }
}
