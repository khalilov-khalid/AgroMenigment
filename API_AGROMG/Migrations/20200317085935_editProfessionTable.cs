using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editProfessionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowStatus",
                table: "Professions",
                newName: "Respondent");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Professions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professions_CompanyId",
                table: "Professions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professions_Companies_CompanyId",
                table: "Professions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professions_Companies_CompanyId",
                table: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Professions_CompanyId",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Professions");

            migrationBuilder.RenameColumn(
                name: "Respondent",
                table: "Professions",
                newName: "ShowStatus");
        }
    }
}
