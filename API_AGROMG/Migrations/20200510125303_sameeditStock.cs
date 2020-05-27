using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class sameeditStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOperations_Purchases_PurchaseId",
                table: "StockOperations");

            migrationBuilder.DropIndex(
                name: "IX_StockOperations_PurchaseId",
                table: "StockOperations");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "StockOperations");

            migrationBuilder.AddColumn<bool>(
                name: "UsedStatus",
                table: "Stocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OperationNumber",
                table: "StockOperations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedStatus",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "OperationNumber",
                table: "StockOperations");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "StockOperations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockOperations_PurchaseId",
                table: "StockOperations",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOperations_Purchases_PurchaseId",
                table: "StockOperations",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
