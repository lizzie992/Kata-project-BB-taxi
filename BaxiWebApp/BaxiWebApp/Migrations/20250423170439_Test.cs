using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37902b77-d530-4a16-9bf2-cbba8b594d93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47feccea-b766-4994-bbde-e671a420af0a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "045778c7-4195-45ce-ac8a-040bdabba5a0", "bb204247-226a-437e-a1fc-fc4efa0b7118", "Admin", "ADMIN" },
                    { "f8b0cdc8-d8bd-48c6-af22-506fb60e2858", "6dcb64d4-6d95-495e-ba03-6fe9dedc0fc2", "Regular", "REGULAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "045778c7-4195-45ce-ac8a-040bdabba5a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8b0cdc8-d8bd-48c6-af22-506fb60e2858");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37902b77-d530-4a16-9bf2-cbba8b594d93", "ca9d4e9f-d0e3-48fa-bc55-d54a870161d6", "Regular", "REGULAR" },
                    { "47feccea-b766-4994-bbde-e671a420af0a", "4aa4a1cf-3a14-4b4a-9204-118f7c145cbf", "Admin", "ADMIN" }
                });
        }
    }
}
