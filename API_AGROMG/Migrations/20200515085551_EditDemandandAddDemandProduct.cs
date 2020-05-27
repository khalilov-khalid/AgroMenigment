using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditDemandandAddDemandProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Country_CountryId",
                table: "Demands");

            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Parcels_ParcelId",
                table: "Demands");

            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Workers_WorkersId",
                table: "Demands");

            migrationBuilder.DropIndex(
                name: "IX_Demands_CountryId",
                table: "Demands");

            migrationBuilder.DropIndex(
                name: "IX_Demands_ParcelId",
                table: "Demands");

            migrationBuilder.DropIndex(
                name: "IX_Demands_WorkersId",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "ParcelId",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "RequiredDate",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "WorkersId",
                table: "Demands");

            migrationBuilder.AddColumn<string>(
                name: "DemandNumber",
                table: "Demands",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DemandProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DemandId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    ParcelId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    WorkersId = table.Column<int>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    RequiredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandProducts_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandProducts_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandProducts_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandProducts_Workers_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemandProducts_CountryId",
                table: "DemandProducts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandProducts_DemandId",
                table: "DemandProducts",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandProducts_ParcelId",
                table: "DemandProducts",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandProducts_ProductId",
                table: "DemandProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandProducts_WorkersId",
                table: "DemandProducts",
                column: "WorkersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandProducts");

            migrationBuilder.DropColumn(
                name: "DemandNumber",
                table: "Demands");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Demands",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Demands",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParcelId",
                table: "Demands",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Demands",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredDate",
                table: "Demands",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkersId",
                table: "Demands",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demands_CountryId",
                table: "Demands",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_ParcelId",
                table: "Demands",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_WorkersId",
                table: "Demands",
                column: "WorkersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Country_CountryId",
                table: "Demands",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Parcels_ParcelId",
                table: "Demands",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Workers_WorkersId",
                table: "Demands",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
