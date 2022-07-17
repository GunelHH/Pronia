using Microsoft.EntityFrameworkCore.Migrations;

namespace ProniaApp.Migrations
{
    public partial class AddedPlantInformationTableWithRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantInformationId",
                table: "Plants",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlantInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shipping = table.Column<string>(nullable: true),
                    AboutReturnRequest = table.Column<string>(nullable: true),
                    Guarantee = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantInformations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantInformationId",
                table: "Plants",
                column: "PlantInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_PlantInformations_PlantInformationId",
                table: "Plants",
                column: "PlantInformationId",
                principalTable: "PlantInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_PlantInformations_PlantInformationId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "PlantInformations");

            migrationBuilder.DropIndex(
                name: "IX_Plants_PlantInformationId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "PlantInformationId",
                table: "Plants");
        }
    }
}
