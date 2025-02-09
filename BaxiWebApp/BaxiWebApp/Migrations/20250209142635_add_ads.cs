using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class add_ads : Migration
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
                    AdType = table.Column<int>(type: "INTEGER", nullable: false),
                    Route = table.Column<string>(type: "TEXT", nullable: false),
                    pickUpDateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    pickUpDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    pickUpTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_AdID",
                table: "Ads",
                column: "AdID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");
        }
    }
}
