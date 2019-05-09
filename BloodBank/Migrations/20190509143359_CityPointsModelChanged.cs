using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodBank.Migrations
{
    public partial class CityPointsModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HtmlContent",
                table: "CityPoints",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CityPoints",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "CityPoints",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CityPoints");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "CityPoints");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "CityPoints",
                newName: "HtmlContent");
        }
    }
}
