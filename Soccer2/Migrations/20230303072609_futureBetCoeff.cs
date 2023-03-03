using Microsoft.EntityFrameworkCore.Migrations;

namespace Soccer2.Migrations
{
    public partial class futureBetCoeff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Coefficient",
                table: "FutureBet",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coefficient",
                table: "FutureBet");
        }
    }
}
