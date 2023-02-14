using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class addBetInfoLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "BetInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "League",
                table: "BetInfo");
        }
    }
}
