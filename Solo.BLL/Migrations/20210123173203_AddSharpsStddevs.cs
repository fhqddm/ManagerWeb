using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class AddSharpsStddevs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Sharp2",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sharp3",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Stddev2",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Stddev3",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sharp2",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "Sharp3",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "Stddev2",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "Stddev3",
                table: "FundInfos");
        }
    }
}
