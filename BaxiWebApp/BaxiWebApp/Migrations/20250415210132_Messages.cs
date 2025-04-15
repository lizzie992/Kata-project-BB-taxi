using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_fromUserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_toUserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversations_ConversationID",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ecd4988-714a-44e6-a5c8-f19f801989a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbfc9ad6-2c07-440c-9eb0-b4b5bf26e34b");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_toUserId",
                table: "Messages",
                newName: "IX_Messages_toUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_fromUserId",
                table: "Messages",
                newName: "IX_Messages_fromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ConversationID",
                table: "Messages",
                newName: "IX_Messages_ConversationID");

            migrationBuilder.AlterColumn<string>(
                name: "toUserId",
                table: "Messages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "fromUserId",
                table: "Messages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad50cb4a-3f23-4d5d-a41d-d127e117dda1", "c125ca49-8597-40fb-a9a3-48693e39d2f5", "Regular", "REGULAR" },
                    { "b246b2c8-425c-40d9-bce1-9398d4f839b2", "8f4d5dfd-3005-428f-b152-fe2d3fc0e297", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_fromUserId",
                table: "Messages",
                column: "fromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_toUserId",
                table: "Messages",
                column: "toUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationID",
                table: "Messages",
                column: "ConversationID",
                principalTable: "Conversations",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_fromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_toUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationID",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad50cb4a-3f23-4d5d-a41d-d127e117dda1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b246b2c8-425c-40d9-bce1-9398d4f839b2");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_toUserId",
                table: "Message",
                newName: "IX_Message_toUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_fromUserId",
                table: "Message",
                newName: "IX_Message_fromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationID",
                table: "Message",
                newName: "IX_Message_ConversationID");

            migrationBuilder.AlterColumn<string>(
                name: "toUserId",
                table: "Message",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "fromUserId",
                table: "Message",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ecd4988-714a-44e6-a5c8-f19f801989a3", "8e32c30a-31f7-488b-97b5-a14bdd261535", "Admin", "ADMIN" },
                    { "dbfc9ad6-2c07-440c-9eb0-b4b5bf26e34b", "e1813126-cc81-47ea-a05e-766c471a7ed6", "Regular", "REGULAR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_fromUserId",
                table: "Message",
                column: "fromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_toUserId",
                table: "Message",
                column: "toUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversations_ConversationID",
                table: "Message",
                column: "ConversationID",
                principalTable: "Conversations",
                principalColumn: "ID");
        }
    }
}
