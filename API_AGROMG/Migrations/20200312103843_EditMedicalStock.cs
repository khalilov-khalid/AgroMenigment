using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditMedicalStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStock_WareHourses_WareHourseId",
                table: "MedicalStock");

            migrationBuilder.DropIndex(
                name: "IX_MedicalStock_WareHourseId",
                table: "MedicalStock");

            migrationBuilder.DropColumn(
                name: "WareHourseId",
                table: "MedicalStock");

            migrationBuilder.AddColumn<int>(
                name: "WareHourse",
                table: "MedicalStock",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WareHourse",
                table: "MedicalStock");

            migrationBuilder.AddColumn<int>(
                name: "WareHourseId",
                table: "MedicalStock",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStock_WareHourseId",
                table: "MedicalStock",
                column: "WareHourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStock_WareHourses_WareHourseId",
                table: "MedicalStock",
                column: "WareHourseId",
                principalTable: "WareHourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
