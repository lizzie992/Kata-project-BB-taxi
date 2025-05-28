using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abd7dc76-4bbe-48b8-981b-d0ece16aa393");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f48bf690-e496-472f-b86f-cca82dcce46e");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Ads",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
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
                    { "465dc2d4-ea98-45cd-a8f7-25bc2899bf57", "fc32b820-9f87-4da4-9bce-dbfabae179cd", "Admin", "ADMIN" },
                    { "b486b659-fd62-49d5-bac5-6f1b32ac21db", "84c97971-5119-464b-848e-88eaf33592b6", "Regular", "REGULAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "465dc2d4-ea98-45cd-a8f7-25bc2899bf57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b486b659-fd62-49d5-bac5-6f1b32ac21db");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Ads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Ads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
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
    }
}
