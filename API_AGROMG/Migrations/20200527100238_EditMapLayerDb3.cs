using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditMapLayerDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapLayers_Parcels_ParcelId",
                table: "MapLayers");

            migrationBuilder.RenameColumn(
                name: "ParcelId",
                table: "MapLayers",
                newName: "ParcelID");

            migrationBuilder.RenameIndex(
                name: "IX_MapLayers_ParcelId",
                table: "MapLayers",
                newName: "IX_MapLayers_ParcelID");

            migrationBuilder.AddForeignKey(
                name: "FK_MapLayers_Parcels_ParcelID",
                table: "MapLayers",
                column: "ParcelID",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapLayers_Parcels_ParcelID",
                table: "MapLayers");

            migrationBuilder.RenameColumn(
                name: "ParcelID",
                table: "MapLayers",
                newName: "ParcelId");

            migrationBuilder.RenameIndex(
                name: "IX_MapLayers_ParcelID",
                table: "MapLayers",
                newName: "IX_MapLayers_ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapLayers_Parcels_ParcelId",
                table: "MapLayers",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
