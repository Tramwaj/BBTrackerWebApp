using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class SubBackToPlays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Substitutions");

            
            migrationBuilder.AddColumn<bool>(
                name: "SubbedIn",
                table: "Plays",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Games_GameId1",
                table: "Plays");

            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Players_PlayerId1",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_GameId1",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_PlayerId1",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "SubbedIn",
                table: "Plays");

            migrationBuilder.CreateTable(
                name: "Substitutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubbedIn = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Substitutions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitutions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Substitutions_GameId",
                table: "Substitutions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutions_PlayerId",
                table: "Substitutions",
                column: "PlayerId");
        }
    }
}
