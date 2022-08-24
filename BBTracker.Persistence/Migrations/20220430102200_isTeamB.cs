using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class isTeamB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTeamB",
                table: "Plays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTeamB",
                table: "Plays");
        }
    }
}
