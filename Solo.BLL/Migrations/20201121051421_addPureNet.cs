using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class addPureNet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PureNet",
                table: "FundDailyIDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Seperate",
                table: "FundDailyIDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PureNet",
                table: "FundDailyIDetails");

            migrationBuilder.DropColumn(
                name: "Seperate",
                table: "FundDailyIDetails");
        }
    }
}
