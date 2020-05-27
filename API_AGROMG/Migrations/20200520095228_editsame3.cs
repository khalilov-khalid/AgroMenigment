using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editsame3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalStock");

            migrationBuilder.AddColumn<string>(
                name: "Reproduction",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockType",
                table: "Stocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HandingCarNumber",
                table: "StockOperations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandingPerson",
                table: "StockOperations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CropsId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CropsId",
                table: "Products",
                column: "CropsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Crops_CropsId",
                table: "Products",
                column: "CropsId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Crops_CropsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CropsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Reproduction",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "StockType",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "HandingCarNumber",
                table: "StockOperations");

            migrationBuilder.DropColumn(
                name: "HandingPerson",
                table: "StockOperations");

            migrationBuilder.DropColumn(
                name: "CropsId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "MedicalStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    Count = table.Column<decimal>(nullable: false),
                    Expirydate = table.Column<DateTime>(nullable: false),
                    FertilizerId = table.Column<int>(nullable: true),
                    WareHourse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStock_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalStock_Fertilizer_FertilizerId",
                        column: x => x.FertilizerId,
                        principalTable: "Fertilizer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStock_CompanyId",
                table: "MedicalStock",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStock_FertilizerId",
                table: "MedicalStock",
                column: "FertilizerId");
        }
    }
}
