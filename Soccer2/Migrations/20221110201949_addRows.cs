using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class addRows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AwayTeamName",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DrawCoef",
                table: "Games",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamName",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DrawCoef",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "League",
                table: "Games");
        }
    }
}
