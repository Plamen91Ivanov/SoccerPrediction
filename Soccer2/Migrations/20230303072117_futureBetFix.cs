using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class futureBetFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FutureBet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Probability",
                table: "FutureBet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "FutureBet");

            migrationBuilder.DropColumn(
                name: "Probability",
                table: "FutureBet");
        }
    }
}
