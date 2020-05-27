using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class ReproductionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reproduction",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "ReproductionId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reproductions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reproductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reproductions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ReproductionId",
                table: "Stocks",
                column: "ReproductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reproductions_CompanyId",
                table: "Reproductions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Reproductions_ReproductionId",
                table: "Stocks",
                column: "ReproductionId",
                principalTable: "Reproductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Reproductions_ReproductionId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Reproductions");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ReproductionId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ReproductionId",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Reproduction",
                table: "Stocks",
                nullable: true);
        }
    }
}
