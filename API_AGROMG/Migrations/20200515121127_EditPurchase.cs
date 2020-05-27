using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Country_CountryId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Currencies_CurrencyId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Demands_DemandId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CountryId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "InitialPayment",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "LastPrice",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "VAT",
                table: "Purchases",
                newName: "EstimatedAmountForTransport");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Purchases",
                newName: "EstimatedAmountForCustoms");

            migrationBuilder.RenameColumn(
                name: "LastPayDate",
                table: "Purchases",
                newName: "PaymentLastDate");

            migrationBuilder.RenameColumn(
                name: "DemandId",
                table: "Purchases",
                newName: "PaymentTermId");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Purchases",
                newName: "DeliveryTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DemandId",
                table: "Purchases",
                newName: "IX_Purchases_PaymentTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_CurrencyId",
                table: "Purchases",
                newName: "IX_Purchases_DeliveryTermId");

            migrationBuilder.AddColumn<bool>(
                name: "CustomsInclude",
                table: "Purchases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryPeriod",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstimatedAmountForGoods",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentPeriod",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TransportInclude",
                table: "Purchases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_DeliveryTerms_DeliveryTermId",
                table: "Purchases",
                column: "DeliveryTermId",
                principalTable: "DeliveryTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_PaymentTerms_PaymentTermId",
                table: "Purchases",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_DeliveryTerms_DeliveryTermId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_PaymentTerms_PaymentTermId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CustomsInclude",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DeliveryPeriod",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EstimatedAmountForGoods",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PaymentPeriod",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TransportInclude",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "PaymentTermId",
                table: "Purchases",
                newName: "DemandId");

            migrationBuilder.RenameColumn(
                name: "PaymentLastDate",
                table: "Purchases",
                newName: "LastPayDate");

            migrationBuilder.RenameColumn(
                name: "EstimatedAmountForTransport",
                table: "Purchases",
                newName: "VAT");

            migrationBuilder.RenameColumn(
                name: "EstimatedAmountForCustoms",
                table: "Purchases",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "DeliveryTermId",
                table: "Purchases",
                newName: "CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PaymentTermId",
                table: "Purchases",
                newName: "IX_Purchases_DemandId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_DeliveryTermId",
                table: "Purchases",
                newName: "IX_Purchases_CurrencyId");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Purchases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Purchases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "InitialPayment",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LastPrice",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CountryId",
                table: "Purchases",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Country_CountryId",
                table: "Purchases",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Currencies_CurrencyId",
                table: "Purchases",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Demands_DemandId",
                table: "Purchases",
                column: "DemandId",
                principalTable: "Demands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
