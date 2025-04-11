using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Conversations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Conversations",
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
                    table.PrimaryKey("PK_Conversations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Conversations_Ads_AdID",
                        column: x => x.AdID,
                        principalTable: "Ads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_adOwnerUserId",
                        column: x => x.adOwnerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_contactingUserId",
                        column: x => x.contactingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fromUserId = table.Column<string>(type: "TEXT", nullable: false),
                    toUserId = table.Column<string>(type: "TEXT", nullable: false),
                    messageText = table.Column<string>(type: "TEXT", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConversationsID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_fromUserId",
                        column: x => x.fromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_toUserId",
                        column: x => x.toUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationsID",
                        column: x => x.ConversationsID,
                        principalTable: "Conversations",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e00a390-cf86-447f-9a7e-f40d11ec8996", "f4963337-6588-4306-be22-f0d0a7e4a8c1", "Regular", "REGULAR" },
                    { "6c1df3f1-a2bf-4f28-981a-d4af71798bbf", "5f17abcd-2d3b-41cf-8506-0111dbd2f5b5", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_AdID",
                table: "Conversations",
                column: "AdID");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_adOwnerUserId",
                table: "Conversations",
                column: "adOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_contactingUserId",
                table: "Conversations",
                column: "contactingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationsID",
                table: "Messages",
                column: "ConversationsID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_fromUserId",
                table: "Messages",
                column: "fromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_toUserId",
                table: "Messages",
                column: "toUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e00a390-cf86-447f-9a7e-f40d11ec8996");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c1df3f1-a2bf-4f28-981a-d4af71798bbf");

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
                    ConversationID = table.Column<int>(type: "INTEGER", nullable: true),
                    messageText = table.Column<string>(type: "TEXT", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
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
    }
}
