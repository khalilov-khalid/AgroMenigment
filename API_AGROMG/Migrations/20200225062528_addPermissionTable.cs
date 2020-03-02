using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class addPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Genders_GenderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleContent",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Users",
                newName: "PermissionsGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                newName: "IX_Users_PermissionsGroupsId");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PermissionsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RolContent = table.Column<string>(nullable: true),
                    CanDelete = table.Column<bool>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsGroups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PermissionsGroups_PermissionsGroupsId",
                table: "Users",
                column: "PermissionsGroupsId",
                principalTable: "PermissionsGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PermissionsGroups_PermissionsGroupsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PermissionsGroups");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PermissionsGroupsId",
                table: "Users",
                newName: "GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PermissionsGroupsId",
                table: "Users",
                newName: "IX_Users_GenderId");

            migrationBuilder.AddColumn<string>(
                name: "RoleContent",
                table: "Users",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Genders_GenderId",
                table: "Users",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
