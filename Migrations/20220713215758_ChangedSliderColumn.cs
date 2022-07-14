using Microsoft.EntityFrameworkCore.Migrations;

namespace ProniaApp.Migrations
{
    public partial class ChangedSliderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BGImage",
                table: "Sliders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BGImage",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
