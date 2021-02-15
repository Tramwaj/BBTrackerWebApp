using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class GameOwnership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Games_OwnerId",
                table: "Games",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_OwnerId",
                table: "Games",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_OwnerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_OwnerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Games");
        }
    }
}
