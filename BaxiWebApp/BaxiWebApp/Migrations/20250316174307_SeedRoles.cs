using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2489d044-b994-4263-a5e8-a6c49a631318", "2042c86c-0766-4ac7-bf67-db5eabc46463", "Regular", "REGULAR" },
                    { "4a7e3a24-a326-4dd5-a84f-b5df4dbafd24", "601ac6f1-e508-4d0b-9cfc-533bf8ded512", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2489d044-b994-4263-a5e8-a6c49a631318");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a7e3a24-a326-4dd5-a84f-b5df4dbafd24");
        }
    }
}
