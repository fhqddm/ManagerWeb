using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class moveReturnRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TransactionInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ReturnRate",
                table: "MyFunds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MyFunds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionInfos");

            migrationBuilder.DropColumn(
                name: "ReturnRate",
                table: "MyFunds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MyFunds");
        }
    }
}
