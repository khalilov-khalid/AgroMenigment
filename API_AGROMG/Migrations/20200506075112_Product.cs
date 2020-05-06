using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementUnits_Languages_LanguageId",
                table: "MeasurementUnits");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementUnits_LanguageId",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "MainId",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MeasurementUnits");

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FertilizerKind",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerKind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnitLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    MeasurementUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnitLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementUnitLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeasurementUnitLanguage_MeasurementUnits_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MainIngredientId = table.Column<int>(nullable: true),
                    FertilizerKindId = table.Column<int>(nullable: true),
                    MeasurementUnitId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_FertilizerKind_FertilizerKindId",
                        column: x => x.FertilizerKindId,
                        principalTable: "FertilizerKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MainIngredients_MainIngredientId",
                        column: x => x.MainIngredientId,
                        principalTable: "MainIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MeasurementUnits_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementUnitLanguage_LanguageId",
                table: "MeasurementUnitLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementUnitLanguage_MeasurementUnitId",
                table: "MeasurementUnitLanguage",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FertilizerKindId",
                table: "Products",
                column: "FertilizerKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainIngredientId",
                table: "Products",
                column: "MainIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MeasurementUnitId",
                table: "Products",
                column: "MeasurementUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "MeasurementUnitLanguage");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "FertilizerKind");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "MeasurementUnits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainId",
                table: "MeasurementUnits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MeasurementUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementUnits_LanguageId",
                table: "MeasurementUnits",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementUnits_Languages_LanguageId",
                table: "MeasurementUnits",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
