using Microsoft.EntityFrameworkCore.Migrations;

namespace crmApi.Migrations
{
    public partial class IconName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconName",
                table: "Ratings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconName",
                table: "Ratings");
        }
    }
}
