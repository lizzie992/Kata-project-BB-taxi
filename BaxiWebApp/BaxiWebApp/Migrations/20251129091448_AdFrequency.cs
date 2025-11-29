using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AdFrequency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AdFrequency",
                table: "Ads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PickUpDay",
                table: "Ads",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PickUpTimeFriday",
                table: "Ads",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PickUpTimeMonday",
                table: "Ads",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PickUpTimeThursday",
                table: "Ads",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PickUpTimeTuesday",
                table: "Ads",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PickUpTimeWednesday",
                table: "Ads",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdFrequency",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PickUpDay",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PickUpTimeFriday",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PickUpTimeMonday",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PickUpTimeThursday",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PickUpTimeTuesday",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "PickUpTimeWednesday",
                table: "Ads");

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
                    AdDirection = table.Column<int>(type: "int", nullable: false),
                    AdType = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: true),
                    Longitude = table.Column<double>(type: "double", nullable: true),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    PickUpDay = table.Column<string>(type: "longtext", nullable: false),
                    PickUpDropOffLocation = table.Column<string>(type: "longtext", nullable: false),
                    PickUpTimeFriday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeMonday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeThursday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeTuesday = table.Column<TimeOnly>(type: "time", nullable: false),
                    PickUpTimeWednesday = table.Column<TimeOnly>(type: "time", nullable: false),
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
    }
}
