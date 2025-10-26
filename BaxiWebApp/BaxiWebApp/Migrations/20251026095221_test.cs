using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaxiWebApp.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Ads_AdID",
                table: "Conversations");

            migrationBuilder.AlterColumn<int>(
                name: "AdID",
                table: "Conversations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "AdID",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
