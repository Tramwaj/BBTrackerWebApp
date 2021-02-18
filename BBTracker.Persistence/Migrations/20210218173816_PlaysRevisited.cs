using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class PlaysRevisited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FieldGoalBlockedId",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FieldGoalId",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FieldGoalReboundedId",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Substitutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubbedOut = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_Plays_FieldGoalBlockedId",
                table: "Plays",
                column: "FieldGoalBlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_FieldGoalId",
                table: "Plays",
                column: "FieldGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_FieldGoalReboundedId",
                table: "Plays",
                column: "FieldGoalReboundedId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutions_GameId",
                table: "Substitutions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutions_PlayerId",
                table: "Substitutions",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Plays_FieldGoalBlockedId",
                table: "Plays",
                column: "FieldGoalBlockedId",
                principalTable: "Plays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Plays_FieldGoalId",
                table: "Plays",
                column: "FieldGoalId",
                principalTable: "Plays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Plays_FieldGoalReboundedId",
                table: "Plays",
                column: "FieldGoalReboundedId",
                principalTable: "Plays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Plays_FieldGoalBlockedId",
                table: "Plays");

            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Plays_FieldGoalId",
                table: "Plays");

            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Plays_FieldGoalReboundedId",
                table: "Plays");

            migrationBuilder.DropTable(
                name: "Substitutions");

            migrationBuilder.DropIndex(
                name: "IX_Plays_FieldGoalBlockedId",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_FieldGoalId",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_FieldGoalReboundedId",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "FieldGoalBlockedId",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "FieldGoalId",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "FieldGoalReboundedId",
                table: "Plays");
        }
    }
}
