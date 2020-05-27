using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class WorkPlanFertilizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkPlanTaskFertilizers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkPlanTaskId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlanTaskFertilizers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlanTaskFertilizers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanTaskFertilizers_WorkPlanTasks_WorkPlanTaskId",
                        column: x => x.WorkPlanTaskId,
                        principalTable: "WorkPlanTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTaskFertilizers_ProductId",
                table: "WorkPlanTaskFertilizers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTaskFertilizers_WorkPlanTaskId",
                table: "WorkPlanTaskFertilizers",
                column: "WorkPlanTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkPlanTaskFertilizers");
        }
    }
}
