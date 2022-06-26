using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class moveStrategy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "MyFunds");

            migrationBuilder.AddColumn<string>(
                name: "Strategy",
                table: "FundInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "FundInfos");

            migrationBuilder.AddColumn<string>(
                name: "Strategy",
                table: "MyFunds",
                type: "text",
                nullable: true);
        }
    }
}
