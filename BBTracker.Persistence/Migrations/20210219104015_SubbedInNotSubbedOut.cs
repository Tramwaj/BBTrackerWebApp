using Microsoft.EntityFrameworkCore.Migrations;

namespace BBTracker.Persistence.Migrations
{
    public partial class SubbedInNotSubbedOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubbedOut",
                table: "Substitutions",
                newName: "SubbedIn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubbedIn",
                table: "Substitutions",
                newName: "SubbedOut");
        }
    }
}
