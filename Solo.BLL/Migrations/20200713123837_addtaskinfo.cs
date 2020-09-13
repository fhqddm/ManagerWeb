using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class addtaskinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "TaskInfos",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "TaskInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SkillName",
                table: "TaskInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "TaskInfos");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TaskInfos");

            migrationBuilder.DropColumn(
                name: "SkillName",
                table: "TaskInfos");
        }
    }
}
