using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Ad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2489d044-b994-4263-a5e8-a6c49a631318");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a7e3a24-a326-4dd5-a84f-b5df4dbafd24");

            migrationBuilder.AddColumn<string>(
                name: "NotificationForRoute",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    adOwnerUserId = table.Column<string>(type: "TEXT", nullable: false),
                    contactingUserId = table.Column<string>(type: "TEXT", nullable: false),
                    AdID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Conversation_Ads_AdID",
                        column: x => x.AdID,
                        principalTable: "Ads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Conversation_AspNetUsers_adOwnerUserId",
                        column: x => x.adOwnerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversation_AspNetUsers_contactingUserId",
                        column: x => x.contactingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fromUserId = table.Column<string>(type: "TEXT", nullable: false),
                    toUserId = table.Column<string>(type: "TEXT", nullable: false),
                    messageText = table.Column<string>(type: "TEXT", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConversationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_fromUserId",
                        column: x => x.fromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_toUserId",
                        column: x => x.toUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Conversation_ConversationID",
                        column: x => x.ConversationID,
                        principalTable: "Conversation",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a1bf54e-a587-4fca-8d22-66c7cc0da0cd", "cb49b913-e969-4bed-8637-a39e71793108", "Regular", "REGULAR" },
                    { "60daa918-c1f1-4d92-8d7f-5dc138376d28", "0d9588b2-be4c-4743-8350-a6c4a20a16f2", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_AdID",
                table: "Conversation",
                column: "AdID");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_adOwnerUserId",
                table: "Conversation",
                column: "adOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_contactingUserId",
                table: "Conversation",
                column: "contactingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationID",
                table: "Message",
                column: "ConversationID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_fromUserId",
                table: "Message",
                column: "fromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_toUserId",
                table: "Message",
                column: "toUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a1bf54e-a587-4fca-8d22-66c7cc0da0cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60daa918-c1f1-4d92-8d7f-5dc138376d28");

            migrationBuilder.DropColumn(
                name: "NotificationForRoute",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2489d044-b994-4263-a5e8-a6c49a631318", "2042c86c-0766-4ac7-bf67-db5eabc46463", "Regular", "REGULAR" },
                    { "4a7e3a24-a326-4dd5-a84f-b5df4dbafd24", "601ac6f1-e508-4d0b-9cfc-533bf8ded512", "Admin", "ADMIN" }
                });
        }
    }
}
