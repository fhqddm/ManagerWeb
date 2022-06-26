using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class addTotalUnitMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalUnitMoney",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalUnitMoney",
                table: "FundInfos");
        }
    }
}
