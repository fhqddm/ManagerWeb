using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class remove_ReturnRate_From_FundInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnRate",
                table: "FundInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ReturnRate",
                table: "FundInfos",
                type: "float",
                nullable: true);
        }
    }
}
