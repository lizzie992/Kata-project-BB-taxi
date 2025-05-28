using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class coordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22c64def-6800-4923-b8c8-ef9bc6f493a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf525768-f671-423b-8c5c-ddd44af188b3");

            migrationBuilder.AlterColumn<string>(
                name: "PickUpDropOffLocation",
                table: "Ads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Ads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Ads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abd7dc76-4bbe-48b8-981b-d0ece16aa393", "00797871-71ac-4c87-9fba-930135781364", "Admin", "ADMIN" },
                    { "f48bf690-e496-472f-b86f-cca82dcce46e", "a9c48983-19c9-4001-945e-7b93425e601d", "Regular", "REGULAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abd7dc76-4bbe-48b8-981b-d0ece16aa393");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f48bf690-e496-472f-b86f-cca82dcce46e");

            migrationBuilder.AlterColumn<string>(
                name: "PickUpDropOffLocation",
                table: "Ads",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                table: "Ads",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Ads",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22c64def-6800-4923-b8c8-ef9bc6f493a7", "130e2bbc-12aa-4c7e-bcd5-f06a0e8b1890", "Admin", "ADMIN" },
                    { "bf525768-f671-423b-8c5c-ddd44af188b3", "12144e22-5f9f-492d-907d-10109b020022", "Regular", "REGULAR" }
                });
        }
    }
}
