using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditSame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberCode",
                table: "Purchases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PurchaseId",
                table: "Stocks",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Purchases_PurchaseId",
                table: "Stocks",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Purchases_PurchaseId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_PurchaseId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "NumberCode",
                table: "Purchases");
        }
    }
}
