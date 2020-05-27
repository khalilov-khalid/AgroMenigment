using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditSameStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOperations_Products_ProductId",
                table: "StockOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOperations_WareHourses_WareHourseId",
                table: "StockOperations");

            migrationBuilder.DropIndex(
                name: "IX_StockOperations_ProductId",
                table: "StockOperations");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "StockOperations");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "StockOperations");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StockOperations");

            migrationBuilder.RenameColumn(
                name: "WareHourseId",
                table: "StockOperations",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockOperations_WareHourseId",
                table: "StockOperations",
                newName: "IX_StockOperations_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOperations_Stocks_StockId",
                table: "StockOperations",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOperations_Stocks_StockId",
                table: "StockOperations");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "StockOperations",
                newName: "WareHourseId");

            migrationBuilder.RenameIndex(
                name: "IX_StockOperations_StockId",
                table: "StockOperations",
                newName: "IX_StockOperations_WareHourseId");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "StockOperations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "StockOperations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "StockOperations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockOperations_ProductId",
                table: "StockOperations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOperations_Products_ProductId",
                table: "StockOperations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOperations_WareHourses_WareHourseId",
                table: "StockOperations",
                column: "WareHourseId",
                principalTable: "WareHourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
