using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class ttjjdata1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Maxretra",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sharp",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Stddev",
                table: "FundInfos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Maxretra",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "Sharp",
                table: "FundInfos");

            migrationBuilder.DropColumn(
                name: "Stddev",
                table: "FundInfos");
        }
    }
}
