using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class WorkPlanEdit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParcelId",
                table: "WorkPlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_ParcelId",
                table: "WorkPlans",
                column: "ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Parcels_ParcelId",
                table: "WorkPlans",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Parcels_ParcelId",
                table: "WorkPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkPlans_ParcelId",
                table: "WorkPlans");

            migrationBuilder.DropColumn(
                name: "ParcelId",
                table: "WorkPlans");
        }
    }
}
