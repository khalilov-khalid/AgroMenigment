using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class PRofessionLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminStatus",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Professions");

            migrationBuilder.AddColumn<string>(
                name: "EndReason",
                table: "WorkerProfessions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfessionLanguanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfessionId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionLanguanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionLanguanges_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfessionLanguanges_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionLanguanges_LanguageId",
                table: "ProfessionLanguanges",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionLanguanges_ProfessionId",
                table: "ProfessionLanguanges",
                column: "ProfessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessionLanguanges");

            migrationBuilder.DropColumn(
                name: "EndReason",
                table: "WorkerProfessions");

            migrationBuilder.AddColumn<bool>(
                name: "AdminStatus",
                table: "Workers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Professions",
                nullable: true);
        }
    }
}
