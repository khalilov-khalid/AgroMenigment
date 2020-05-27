using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class PurchaseComingQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "StockWaitingProduct");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "StockWaitingProduct");

            migrationBuilder.AddColumn<decimal>(
                name: "ComingQuantity",
                table: "PurchaseProducts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComingQuantity",
                table: "PurchaseProducts");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "StockWaitingProduct",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "StockWaitingProduct",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
