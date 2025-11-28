using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class recurringAds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecurringAdID",
                table: "Conversations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurringAdID",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecurringAds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AdOwnerId = table.Column<string>(type: "varchar(255)", nullable: true),
                    AdType = table.Column<int>(type: "int", nullable: false),
                    AdDirection = table.Column<int>(type: "int", nullable: false),
                    PickUpDropOffLocation = table.Column<string>(type: "longtext", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: true),
                    Longitude = table.Column<double>(type: "double", nullable: true),
                    PickUpDay = table.Column<string>(type: "longtext", nullable: false),
                    PickUpTimeMonday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeTuesday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeWednesday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeThursday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeFriday = table.Column<TimeOnly>(type: "time", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    SpecificRequests = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringAds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecurringAds_AspNetUsers_AdOwnerId",
                        column: x => x.AdOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RecurringAdID",
                table: "Conversations",
                column: "RecurringAdID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_RecurringAdID",
                table: "Ads",
                column: "RecurringAdID");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringAds_AdOwnerId",
                table: "RecurringAds",
                column: "AdOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_RecurringAds_RecurringAdID",
                table: "Ads",
                column: "RecurringAdID",
                principalTable: "RecurringAds",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_RecurringAds_RecurringAdID",
                table: "Conversations",
                column: "RecurringAdID",
                principalTable: "RecurringAds",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_RecurringAds_RecurringAdID",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_RecurringAds_RecurringAdID",
                table: "Conversations");

            migrationBuilder.DropTable(
                name: "RecurringAds");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_RecurringAdID",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Ads_RecurringAdID",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "RecurringAdID",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "RecurringAdID",
                table: "Ads");
        }
    }
}
