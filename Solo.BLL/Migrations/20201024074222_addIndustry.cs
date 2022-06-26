using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class addIndustry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetProfit",
                table: "StockInfos");

            migrationBuilder.AddColumn<string>(
                name: "IndustryId",
                table: "StockInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryName",
                table: "StockInfos",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NetRate",
                table: "StockInfos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "StockInfos");

            migrationBuilder.DropColumn(
                name: "IndustryName",
                table: "StockInfos");

            migrationBuilder.DropColumn(
                name: "NetRate",
                table: "StockInfos");

            migrationBuilder.AddColumn<double>(
                name: "NetProfit",
                table: "StockInfos",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
