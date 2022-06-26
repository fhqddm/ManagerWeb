using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class AddStockPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StockPositions",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Slope20",
                table: "FundDailyIDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockPositions",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "Slope20",
                table: "FundDailyIDetails");
        }
    }
}
