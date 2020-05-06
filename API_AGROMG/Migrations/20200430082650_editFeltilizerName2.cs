using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editFeltilizerName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStock_Fertilizer_NameOfDrugId",
                table: "MedicalStock");

            migrationBuilder.RenameColumn(
                name: "NameOfDrugId",
                table: "MedicalStock",
                newName: "FertilizerId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalStock_NameOfDrugId",
                table: "MedicalStock",
                newName: "IX_MedicalStock_FertilizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStock_Fertilizer_FertilizerId",
                table: "MedicalStock",
                column: "FertilizerId",
                principalTable: "Fertilizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStock_Fertilizer_FertilizerId",
                table: "MedicalStock");

            migrationBuilder.RenameColumn(
                name: "FertilizerId",
                table: "MedicalStock",
                newName: "NameOfDrugId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalStock_FertilizerId",
                table: "MedicalStock",
                newName: "IX_MedicalStock_NameOfDrugId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStock_Fertilizer_NameOfDrugId",
                table: "MedicalStock",
                column: "NameOfDrugId",
                principalTable: "Fertilizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
