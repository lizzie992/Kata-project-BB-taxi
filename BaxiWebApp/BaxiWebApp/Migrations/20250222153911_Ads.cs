using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Ads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdOwnerId = table.Column<string>(type: "TEXT", nullable: true),
                    AdType = table.Column<int>(type: "INTEGER", nullable: false),
                    Route = table.Column<string>(type: "TEXT", nullable: false),
                    pickUpDateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecificRequests = table.Column<string>(type: "TEXT", nullable: false),
                    AdID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ads_Ads_AdID",
                        column: x => x.AdID,
                        principalTable: "Ads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ads_AspNetUsers_AdOwnerId",
                        column: x => x.AdOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_AdID",
                table: "Ads",
                column: "AdID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_AdOwnerId",
                table: "Ads",
                column: "AdOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");
        }
    }
}
