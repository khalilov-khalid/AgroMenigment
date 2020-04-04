using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class AddMedicalStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<int>(nullable: false),
                    Count = table.Column<decimal>(nullable: false),
                    NameOfDrugId = table.Column<int>(nullable: true),
                    WareHourseId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    Expirydate = table.Column<DateTime>(nullable: false)
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
                        name: "FK_MedicalStock_NameOfDrugs_NameOfDrugId",
                        column: x => x.NameOfDrugId,
                        principalTable: "NameOfDrugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalStock_WareHourses_WareHourseId",
                        column: x => x.WareHourseId,
                        principalTable: "WareHourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStock_CompanyId",
                table: "MedicalStock",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStock_NameOfDrugId",
                table: "MedicalStock",
                column: "NameOfDrugId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStock_WareHourseId",
                table: "MedicalStock",
                column: "WareHourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalStock");
        }
    }
}
