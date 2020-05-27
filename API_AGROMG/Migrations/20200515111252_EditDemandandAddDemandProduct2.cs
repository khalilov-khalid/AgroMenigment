using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditDemandandAddDemandProduct2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedId",
                table: "Demands",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demands_CreatedId",
                table: "Demands",
                column: "CreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Workers_CreatedId",
                table: "Demands",
                column: "CreatedId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Workers_CreatedId",
                table: "Demands");

            migrationBuilder.DropIndex(
                name: "IX_Demands_CreatedId",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "CreatedId",
                table: "Demands");
        }
    }
}
