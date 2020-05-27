using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class addbarcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "StockOperations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "StockOperations");
        }
    }
}
