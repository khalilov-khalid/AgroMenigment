using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class MainIngedientsandDrugsedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "NameOfDrugs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MainIngredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NameOfDrugs_CompanyId",
                table: "NameOfDrugs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MainIngredients_CompanyId",
                table: "MainIngredients",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainIngredients_Companies_CompanyId",
                table: "MainIngredients",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NameOfDrugs_Companies_CompanyId",
                table: "NameOfDrugs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainIngredients_Companies_CompanyId",
                table: "MainIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_NameOfDrugs_Companies_CompanyId",
                table: "NameOfDrugs");

            migrationBuilder.DropIndex(
                name: "IX_NameOfDrugs_CompanyId",
                table: "NameOfDrugs");

            migrationBuilder.DropIndex(
                name: "IX_MainIngredients_CompanyId",
                table: "MainIngredients");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "NameOfDrugs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MainIngredients");
        }
    }
}
