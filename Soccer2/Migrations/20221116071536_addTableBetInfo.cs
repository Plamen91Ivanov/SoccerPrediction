using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class addTableBetInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetInfo",
                columns: table => new
                {
                    BetInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeam = table.Column<string>(nullable: true),
                    AwayTeam = table.Column<string>(nullable: true),
                    HomeResult = table.Column<int>(nullable: false),
                    AwayResult = table.Column<int>(nullable: false),
                    HomeResultHalfTime = table.Column<int>(nullable: false),
                    AwayResultHalfTime = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    ResultHT = table.Column<string>(nullable: true),
                    HomeCoef = table.Column<double>(nullable: false),
                    DrawCoef = table.Column<double>(nullable: false),
                    AWayCoef = table.Column<double>(nullable: false),
                    Winner = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Bet = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetInfo", x => x.BetInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetInfo");
        }
    }
}
