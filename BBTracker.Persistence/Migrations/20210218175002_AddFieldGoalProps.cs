using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class AddFieldGoalProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Made",
                table: "Plays",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Plays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WasAssisted",
                table: "Plays",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WasBlocked",
                table: "Plays",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Made",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "WasAssisted",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "WasBlocked",
                table: "Plays");
        }
    }
}
