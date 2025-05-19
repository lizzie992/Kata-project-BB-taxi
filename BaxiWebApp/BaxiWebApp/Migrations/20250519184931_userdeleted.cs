using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class userdeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca03d509-a19d-4416-8eca-081fffdd4bef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7c0f573-f934-4b23-acf3-abf263cfc0d8");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3dbfe283-6f5e-4564-84da-b9837f79ae14", "a2bd4d6c-7463-4c19-87a6-1252abe1ceef", "Regular", "REGULAR" },
                    { "f3d47fe3-1cbd-4f3b-82cc-eb01e6969c75", "af2030b6-0a4d-40a9-b13b-a73008d4de70", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dbfe283-6f5e-4564-84da-b9837f79ae14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3d47fe3-1cbd-4f3b-82cc-eb01e6969c75");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ca03d509-a19d-4416-8eca-081fffdd4bef", "ad8aee3e-a2d1-4989-aaa6-baa6773c2315", "Admin", "ADMIN" },
                    { "f7c0f573-f934-4b23-acf3-abf263cfc0d8", "479ae222-c0e8-4632-b68c-d6869390f2ab", "Regular", "REGULAR" }
                });
        }
    }
}
