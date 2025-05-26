using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class pleasework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Coordinates_PickUpDropOffCoordinatesID",
                table: "Ads");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Ads_PickUpDropOffCoordinatesID",
                table: "Ads");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ad2322b-87b6-4b21-85a5-bf88c86e3f12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4dbb8752-0438-4a15-82ae-28ed302b8595");

            migrationBuilder.DropColumn(
                name: "PickUpDropOffCoordinatesID",
                table: "Ads");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Ads",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Ads",
                type: "REAL",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22c64def-6800-4923-b8c8-ef9bc6f493a7", "130e2bbc-12aa-4c7e-bcd5-f06a0e8b1890", "Admin", "ADMIN" },
                    { "bf525768-f671-423b-8c5c-ddd44af188b3", "12144e22-5f9f-492d-907d-10109b020022", "Regular", "REGULAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22c64def-6800-4923-b8c8-ef9bc6f493a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf525768-f671-423b-8c5c-ddd44af188b3");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Ads");

            migrationBuilder.AddColumn<int>(
                name: "PickUpDropOffCoordinatesID",
                table: "Ads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<float>(type: "REAL", nullable: false),
                    Longitude = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ad2322b-87b6-4b21-85a5-bf88c86e3f12", "4c90696e-ae66-42d6-bbee-3ef0b937f304", "Admin", "ADMIN" },
                    { "4dbb8752-0438-4a15-82ae-28ed302b8595", "f2bbddd0-6303-4853-bce7-bdf0761279ca", "Regular", "REGULAR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_PickUpDropOffCoordinatesID",
                table: "Ads",
                column: "PickUpDropOffCoordinatesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Coordinates_PickUpDropOffCoordinatesID",
                table: "Ads",
                column: "PickUpDropOffCoordinatesID",
                principalTable: "Coordinates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
