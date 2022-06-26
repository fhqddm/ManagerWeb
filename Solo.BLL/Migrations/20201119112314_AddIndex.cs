using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Volatility",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "MA10",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MA20",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MA5",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MA60",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Slope10",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Slope60",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Trend10",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Trend20",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Trend60",
                table: "FundDailyIDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volatility",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "MA10",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "MA20",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "MA5",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "MA60",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "Slope10",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "Slope60",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "Trend10",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "Trend20",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "Trend60",
                table: "FundDailyIDetails");
        }
    }
}
