using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class moveEstimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "MyFunds");

            migrationBuilder.DropColumn(
                name: "ValuationId",
                table: "MyFunds");

            migrationBuilder.AddColumn<double>(
                name: "Estimate",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ValuationId",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "ValuationId",
                table: "FundInfos");

            migrationBuilder.AddColumn<double>(
                name: "Estimate",
                table: "MyFunds",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ValuationId",
                table: "MyFunds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
