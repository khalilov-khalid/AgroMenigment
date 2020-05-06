using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class renameUserstoWorker2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfessions_Workers_UserId",
                table: "UserProfessions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProfessions",
                newName: "WorkersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfessions_UserId",
                table: "UserProfessions",
                newName: "IX_UserProfessions_WorkersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfessions_Workers_WorkersId",
                table: "UserProfessions",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfessions_Workers_WorkersId",
                table: "UserProfessions");

            migrationBuilder.RenameColumn(
                name: "WorkersId",
                table: "UserProfessions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfessions_WorkersId",
                table: "UserProfessions",
                newName: "IX_UserProfessions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfessions_Workers_UserId",
                table: "UserProfessions",
                column: "UserId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
