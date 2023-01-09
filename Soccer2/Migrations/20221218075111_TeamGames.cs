using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class TeamGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BetCoef",
                table: "BetInfo",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "BetType",
                table: "BetInfo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BetInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "WinPrice",
                table: "BetInfo",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamId",
                table: "Games",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamId",
                table: "Games",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "BetCoef",
                table: "BetInfo");

            migrationBuilder.DropColumn(
                name: "BetType",
                table: "BetInfo");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BetInfo");

            migrationBuilder.DropColumn(
                name: "WinPrice",
                table: "BetInfo");
        }
    }
}
