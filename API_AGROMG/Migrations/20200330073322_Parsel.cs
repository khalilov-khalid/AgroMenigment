using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class Parsel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParcelCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParcelCategoryLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParcelCategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelCategoryLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelCategoryLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParcelCategoryLanguages_ParcelCategories_ParcelCategoryId",
                        column: x => x.ParcelCategoryId,
                        principalTable: "ParcelCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParcelCategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Area = table.Column<decimal>(nullable: false),
                    CropsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_Crops_CropsId",
                        column: x => x.CropsId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_ParcelCategories_ParcelCategoryId",
                        column: x => x.ParcelCategoryId,
                        principalTable: "ParcelCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParcelCategoryLanguages_LanguageId",
                table: "ParcelCategoryLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelCategoryLanguages_ParcelCategoryId",
                table: "ParcelCategoryLanguages",
                column: "ParcelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CropsId",
                table: "Parcels",
                column: "CropsId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelCategoryId",
                table: "Parcels",
                column: "ParcelCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelCategoryLanguages");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "ParcelCategories");
        }
    }
}
