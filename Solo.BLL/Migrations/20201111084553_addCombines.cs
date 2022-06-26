using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class addCombines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Combines",
                table: "FundInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combines",
                table: "FundInfos");
        }
    }
}
