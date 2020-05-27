using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class WorkPlanEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Workers_RespondentId",
                table: "WorkPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkPlans_RespondentId",
                table: "WorkPlans");

            migrationBuilder.DropColumn(
                name: "RespondentId",
                table: "WorkPlans");

            migrationBuilder.AddColumn<int>(
                name: "RespondentId",
                table: "WorkPlanTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTasks_RespondentId",
                table: "WorkPlanTasks",
                column: "RespondentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlanTasks_Workers_RespondentId",
                table: "WorkPlanTasks",
                column: "RespondentId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlanTasks_Workers_RespondentId",
                table: "WorkPlanTasks");

            migrationBuilder.DropIndex(
                name: "IX_WorkPlanTasks_RespondentId",
                table: "WorkPlanTasks");

            migrationBuilder.DropColumn(
                name: "RespondentId",
                table: "WorkPlanTasks");

            migrationBuilder.AddColumn<int>(
                name: "RespondentId",
                table: "WorkPlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_RespondentId",
                table: "WorkPlans",
                column: "RespondentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Workers_RespondentId",
                table: "WorkPlans",
                column: "RespondentId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
