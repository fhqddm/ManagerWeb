using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class addFundTop10HoldRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FundTop10HoldRate",
                table: "StockInfos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundTop10HoldRate",
                table: "StockInfos");
        }
    }
}
