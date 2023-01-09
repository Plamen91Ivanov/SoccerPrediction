using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class gameInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamesInfo",
                columns: table => new
                {
                    GameInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    HomeResult = table.Column<int>(nullable: false),
                    AwayResult = table.Column<int>(nullable: false),
                    HomeCoef = table.Column<double>(nullable: false),
                    AwayCoef = table.Column<double>(nullable: false),
                    DrawCoef = table.Column<double>(nullable: false),
                    Winner = table.Column<string>(nullable: true),
                    HomeTeam = table.Column<string>(nullable: true),
                    AwayTeam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesInfo", x => x.GameInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesInfo");
        }
    }
}
