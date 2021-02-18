using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class BatchMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Games_GameId1",
                table: "Plays");

            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Players_PlayerId",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_GameId1",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Plays");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Plays_GameId",
                table: "Plays",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Games_GameId",
                table: "Plays",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Players_PlayerId",
                table: "Plays",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Games_GameId",
                table: "Plays");

            migrationBuilder.DropForeignKey(
                name: "FK_Plays_Players_PlayerId",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_GameId",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Plays");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId1",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plays_GameId1",
                table: "Plays",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Games_GameId1",
                table: "Plays",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_Players_PlayerId",
                table: "Plays",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
