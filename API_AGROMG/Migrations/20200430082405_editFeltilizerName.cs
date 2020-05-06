using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editFeltilizerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStock_NameOfDrugs_NameOfDrugId",
                table: "MedicalStock");

            migrationBuilder.DropTable(
                name: "NameOfDrugs");

            migrationBuilder.CreateTable(
                name: "Fertilizer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    MainIngredientId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    MeasurementUnit = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fertilizer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fertilizer_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fertilizer_MainIngredients_MainIngredientId",
                        column: x => x.MainIngredientId,
                        principalTable: "MainIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fertilizer_CompanyId",
                table: "Fertilizer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Fertilizer_MainIngredientId",
                table: "Fertilizer",
                column: "MainIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStock_Fertilizer_NameOfDrugId",
                table: "MedicalStock",
                column: "NameOfDrugId",
                principalTable: "Fertilizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStock_Fertilizer_NameOfDrugId",
                table: "MedicalStock");

            migrationBuilder.DropTable(
                name: "Fertilizer");

            migrationBuilder.CreateTable(
                name: "NameOfDrugs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    MainIngredientId = table.Column<int>(nullable: true),
                    MeasurementUnit = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameOfDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameOfDrugs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NameOfDrugs_MainIngredients_MainIngredientId",
                        column: x => x.MainIngredientId,
                        principalTable: "MainIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NameOfDrugs_CompanyId",
                table: "NameOfDrugs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NameOfDrugs_MainIngredientId",
                table: "NameOfDrugs",
                column: "MainIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStock_NameOfDrugs_NameOfDrugId",
                table: "MedicalStock",
                column: "NameOfDrugId",
                principalTable: "NameOfDrugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
