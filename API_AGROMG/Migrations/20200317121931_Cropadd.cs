using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class Cropadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CropCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CropCategoryLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CropCategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropCategoryLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropCategoryLanguages_CropCategories_CropCategoryId",
                        column: x => x.CropCategoryId,
                        principalTable: "CropCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CropCategoryLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CropCategoryId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crops_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crops_CropCategories_CropCategoryId",
                        column: x => x.CropCategoryId,
                        principalTable: "CropCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CropLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CropsId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropLanguages_Crops_CropsId",
                        column: x => x.CropsId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CropLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CropCategoryLanguages_CropCategoryId",
                table: "CropCategoryLanguages",
                column: "CropCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CropCategoryLanguages_LanguageId",
                table: "CropCategoryLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CropLanguages_CropsId",
                table: "CropLanguages",
                column: "CropsId");

            migrationBuilder.CreateIndex(
                name: "IX_CropLanguages_LanguageId",
                table: "CropLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_CompanyId",
                table: "Crops",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_CropCategoryId",
                table: "Crops",
                column: "CropCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CropCategoryLanguages");

            migrationBuilder.DropTable(
                name: "CropLanguages");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "CropCategories");
        }
    }
}
