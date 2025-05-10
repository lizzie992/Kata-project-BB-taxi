using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class active : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13b2916f-07fb-406f-8363-1685866f63f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "305dc3d1-048a-4c68-b376-111d9d2e50cf");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ca03d509-a19d-4416-8eca-081fffdd4bef", "ad8aee3e-a2d1-4989-aaa6-baa6773c2315", "Admin", "ADMIN" },
                    { "f7c0f573-f934-4b23-acf3-abf263cfc0d8", "479ae222-c0e8-4632-b68c-d6869390f2ab", "Regular", "REGULAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca03d509-a19d-4416-8eca-081fffdd4bef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7c0f573-f934-4b23-acf3-abf263cfc0d8");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13b2916f-07fb-406f-8363-1685866f63f3", "e7735dc7-c795-44de-a790-8ff4c4c3116e", "Admin", "ADMIN" },
                    { "305dc3d1-048a-4c68-b376-111d9d2e50cf", "a7bc2bcb-6fa2-4a7e-b19e-a58c601bda7f", "Regular", "REGULAR" }
                });
        }
    }
}
