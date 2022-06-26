using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class add2022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "A2022",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "R2022",
                table: "FundInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A2022",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "R2022",
                table: "FundInfos");
        }
    }
}
