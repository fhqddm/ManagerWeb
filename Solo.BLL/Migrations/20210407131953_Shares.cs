using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class Shares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Shares",
                table: "TransactionInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "MyFunds",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HoldShares",
                table: "MyFunds",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalBonus",
                table: "MyFunds",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shares",
                table: "TransactionInfos");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "MyFunds");

            migrationBuilder.DropColumn(
                name: "HoldShares",
                table: "MyFunds");

            migrationBuilder.DropColumn(
                name: "TotalBonus",
                table: "MyFunds");
        }
    }
}
