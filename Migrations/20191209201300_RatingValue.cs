using Microsoft.EntityFrameworkCore.Migrations;

namespace crmApi.Migrations
{
    public partial class RatingValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Companies",
                newName: "RatingValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatingValue",
                table: "Companies",
                newName: "Rating");
        }
    }
}
