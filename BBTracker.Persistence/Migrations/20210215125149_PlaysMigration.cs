using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class PlaysMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Play");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Plays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsTeamB = table.Column<bool>(type: "bit", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayType = table.Column<int>(type: "int", nullable: false),
                    Assist_Points = table.Column<int>(type: "int", nullable: true),
                    ShooterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Block_Points = table.Column<int>(type: "int", nullable: true),
                    BlockedPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    Made = table.Column<bool>(type: "bit", nullable: true),
                    WasBlocked = table.Column<bool>(type: "bit", nullable: true),
                    WasAssisted = table.Column<bool>(type: "bit", nullable: true),
                    FouledPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsOffensive = table.Column<bool>(type: "bit", nullable: true),
                    Rebound_Points = table.Column<int>(type: "int", nullable: true),
                    StolenFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubbedIn = table.Column<bool>(type: "bit", nullable: true),
                    TurnoverCauserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plays_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plays_Players_BlockedPlayerId",
                        column: x => x.BlockedPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plays_Players_FouledPlayerId",
                        column: x => x.FouledPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plays_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plays_Players_ShooterId",
                        column: x => x.ShooterId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plays_Players_StolenFromId",
                        column: x => x.StolenFromId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plays_Players_TurnoverCauserId",
                        column: x => x.TurnoverCauserId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plays_BlockedPlayerId",
                table: "Plays",
                column: "BlockedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_FouledPlayerId",
                table: "Plays",
                column: "FouledPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_GameId",
                table: "Plays",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_PlayerId",
                table: "Plays",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_ShooterId",
                table: "Plays",
                column: "ShooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_StolenFromId",
                table: "Plays",
                column: "StolenFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_TurnoverCauserId",
                table: "Plays",
                column: "TurnoverCauserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plays");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Play",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GameTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsTeamB = table.Column<bool>(type: "bit", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Play", x => x.id);
                    table.ForeignKey(
                        name: "FK_Play_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Play_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Play_GameId",
                table: "Play",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Play_PlayerId",
                table: "Play",
                column: "PlayerId");
        }
    }
}
