using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editTemporaryExselSector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryExsel_TemporaryParcel_TemporaryParcelId",
                table: "TemporaryExsel");

            migrationBuilder.RenameColumn(
                name: "TemporaryParcelId",
                table: "TemporaryExsel",
                newName: "TemporarySectorId");

            migrationBuilder.RenameIndex(
                name: "IX_TemporaryExsel_TemporaryParcelId",
                table: "TemporaryExsel",
                newName: "IX_TemporaryExsel_TemporarySectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryExsel_TemporarySector_TemporarySectorId",
                table: "TemporaryExsel",
                column: "TemporarySectorId",
                principalTable: "TemporarySector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryExsel_TemporarySector_TemporarySectorId",
                table: "TemporaryExsel");

            migrationBuilder.RenameColumn(
                name: "TemporarySectorId",
                table: "TemporaryExsel",
                newName: "TemporaryParcelId");

            migrationBuilder.RenameIndex(
                name: "IX_TemporaryExsel_TemporarySectorId",
                table: "TemporaryExsel",
                newName: "IX_TemporaryExsel_TemporaryParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryExsel_TemporaryParcel_TemporaryParcelId",
                table: "TemporaryExsel",
                column: "TemporaryParcelId",
                principalTable: "TemporaryParcel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
