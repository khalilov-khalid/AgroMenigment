using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "PermissionsGroups");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "PermissionsGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsGroups_CompanyId",
                table: "PermissionsGroups",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsGroups_Companies_CompanyId",
                table: "PermissionsGroups",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsGroups_Companies_CompanyId",
                table: "PermissionsGroups");

            migrationBuilder.DropIndex(
                name: "IX_PermissionsGroups_CompanyId",
                table: "PermissionsGroups");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PermissionsGroups");

            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "PermissionsGroups",
                nullable: false,
                defaultValue: false);
        }
    }
}
