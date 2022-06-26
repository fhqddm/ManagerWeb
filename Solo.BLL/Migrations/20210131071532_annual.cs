using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class annual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "A2015",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "A2016",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "A2017",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "A2018",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "A2019",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "A2020",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "A2021",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "R2021",
                table: "FundInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A2015",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "A2016",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "A2017",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "A2018",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "A2019",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "A2020",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "A2021",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "R2021",
                table: "FundInfos");
        }
    }
}
