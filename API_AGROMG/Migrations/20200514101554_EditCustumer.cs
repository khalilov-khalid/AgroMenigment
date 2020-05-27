using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditCustumer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Customers",
                newName: "PaymentPeriod");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Customers",
                newName: "PaymentAmount");

            migrationBuilder.AddColumn<string>(
                name: "AdvancePaymentAmount",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdvancePaymentKindId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdvancePaymentPeriod",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdvancePaymentTermId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AgreementDate",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AgreementNumber",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryPeriod",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTermId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentKindId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTermId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryTerms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTermLangs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: true),
                    DeliveryTermId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTermLangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryTermLangs_DeliveryTerms_DeliveryTermId",
                        column: x => x.DeliveryTermId,
                        principalTable: "DeliveryTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryTermLangs_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AdvancePaymentKindId",
                table: "Customers",
                column: "AdvancePaymentKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AdvancePaymentTermId",
                table: "Customers",
                column: "AdvancePaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CountryId",
                table: "Customers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeliveryTermId",
                table: "Customers",
                column: "DeliveryTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentKindId",
                table: "Customers",
                column: "PaymentKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentTermId",
                table: "Customers",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTermLangs_DeliveryTermId",
                table: "DeliveryTermLangs",
                column: "DeliveryTermId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTermLangs_LanguageId",
                table: "DeliveryTermLangs",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_PaymentKinds_AdvancePaymentKindId",
                table: "Customers",
                column: "AdvancePaymentKindId",
                principalTable: "PaymentKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_PaymentTerms_AdvancePaymentTermId",
                table: "Customers",
                column: "AdvancePaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Cities_CityId",
                table: "Customers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Country_CountryId",
                table: "Customers",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_DeliveryTerms_DeliveryTermId",
                table: "Customers",
                column: "DeliveryTermId",
                principalTable: "DeliveryTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_PaymentKinds_PaymentKindId",
                table: "Customers",
                column: "PaymentKindId",
                principalTable: "PaymentKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_PaymentTerms_PaymentTermId",
                table: "Customers",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_PaymentKinds_AdvancePaymentKindId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_PaymentTerms_AdvancePaymentTermId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Cities_CityId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Country_CountryId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_DeliveryTerms_DeliveryTermId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_PaymentKinds_PaymentKindId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_PaymentTerms_PaymentTermId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "DeliveryTermLangs");

            migrationBuilder.DropTable(
                name: "DeliveryTerms");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AdvancePaymentKindId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AdvancePaymentTermId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CityId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CountryId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DeliveryTermId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PaymentKindId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PaymentTermId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentAmount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentKindId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentPeriod",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentTermId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AgreementDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AgreementNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeliveryPeriod",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeliveryTermId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentKindId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PaymentPeriod",
                table: "Customers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "Customers",
                newName: "City");
        }
    }
}
