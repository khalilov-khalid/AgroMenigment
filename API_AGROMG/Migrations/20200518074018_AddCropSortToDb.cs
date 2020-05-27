using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class AddCropSortToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CropSorts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CropsId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropSorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropSorts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CropSorts_Crops_CropsId",
                        column: x => x.CropsId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CropSortLangs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CropSortId = table.Column<int>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropSortLangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropSortLangs_CropSorts_CropSortId",
                        column: x => x.CropSortId,
                        principalTable: "CropSorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CropSortLangs_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CropSortLangs_CropSortId",
                table: "CropSortLangs",
                column: "CropSortId");

            migrationBuilder.CreateIndex(
                name: "IX_CropSortLangs_LanguageId",
                table: "CropSortLangs",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CropSorts_CompanyId",
                table: "CropSorts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CropSorts_CropsId",
                table: "CropSorts",
                column: "CropsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CropSortLangs");

            migrationBuilder.DropTable(
                name: "CropSorts");
        }
    }
}
