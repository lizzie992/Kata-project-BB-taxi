using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Conversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationID",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad50cb4a-3f23-4d5d-a41d-d127e117dda1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b246b2c8-425c-40d9-bce1-9398d4f839b2");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationID",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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
                    { "37902b77-d530-4a16-9bf2-cbba8b594d93", "ca9d4e9f-d0e3-48fa-bc55-d54a870161d6", "Regular", "REGULAR" },
                    { "47feccea-b766-4994-bbde-e671a420af0a", "4aa4a1cf-3a14-4b4a-9204-118f7c145cbf", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations",
                column: "AdID",
                principalTable: "Ads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationID",
                table: "Messages",
                column: "ConversationID",
                principalTable: "Conversations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationID",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37902b77-d530-4a16-9bf2-cbba8b594d93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47feccea-b766-4994-bbde-e671a420af0a");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationID",
                table: "Messages",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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
                    { "ad50cb4a-3f23-4d5d-a41d-d127e117dda1", "c125ca49-8597-40fb-a9a3-48693e39d2f5", "Regular", "REGULAR" },
                    { "b246b2c8-425c-40d9-bce1-9398d4f839b2", "8f4d5dfd-3005-428f-b152-fe2d3fc0e297", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations",
                column: "AdID",
                principalTable: "Ads",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationID",
                table: "Messages",
                column: "ConversationID",
                principalTable: "Conversations",
                principalColumn: "ID");
        }
    }
}
