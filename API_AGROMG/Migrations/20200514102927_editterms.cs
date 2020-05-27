using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editterms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "PaymentTerms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "PaymentKinds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PaymentKinds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DeliveryTerms",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "DeliveryTermLangs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_CompanyId",
                table: "PaymentTerms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentKinds_CompanyId",
                table: "PaymentKinds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTerms_CompanyId",
                table: "DeliveryTerms",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryTerms_Companies_CompanyId",
                table: "DeliveryTerms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentKinds_Companies_CompanyId",
                table: "PaymentKinds",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTerms_Companies_CompanyId",
                table: "PaymentTerms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryTerms_Companies_CompanyId",
                table: "DeliveryTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentKinds_Companies_CompanyId",
                table: "PaymentKinds");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTerms_Companies_CompanyId",
                table: "PaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTerms_CompanyId",
                table: "PaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_PaymentKinds_CompanyId",
                table: "PaymentKinds");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryTerms_CompanyId",
                table: "DeliveryTerms");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PaymentKinds");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentKinds");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DeliveryTerms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DeliveryTermLangs");
        }
    }
}
