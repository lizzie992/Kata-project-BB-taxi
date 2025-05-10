using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class testing2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "045778c7-4195-45ce-ac8a-040bdabba5a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8b0cdc8-d8bd-48c6-af22-506fb60e2858");

            migrationBuilder.AlterColumn<int>(
                name: "AdID",
                table: "Conversations",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13b2916f-07fb-406f-8363-1685866f63f3", "e7735dc7-c795-44de-a790-8ff4c4c3116e", "Admin", "ADMIN" },
                    { "305dc3d1-048a-4c68-b376-111d9d2e50cf", "a7bc2bcb-6fa2-4a7e-b19e-a58c601bda7f", "Regular", "REGULAR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations",
                column: "AdID",
                principalTable: "Ads",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13b2916f-07fb-406f-8363-1685866f63f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "305dc3d1-048a-4c68-b376-111d9d2e50cf");

            migrationBuilder.AlterColumn<int>(
                name: "AdID",
                table: "Conversations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "045778c7-4195-45ce-ac8a-040bdabba5a0", "bb204247-226a-437e-a1fc-fc4efa0b7118", "Admin", "ADMIN" },
                    { "f8b0cdc8-d8bd-48c6-af22-506fb60e2858", "6dcb64d4-6d95-495e-ba03-6fe9dedc0fc2", "Regular", "REGULAR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations",
                column: "AdID",
                principalTable: "Ads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
