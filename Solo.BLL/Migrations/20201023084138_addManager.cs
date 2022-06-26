using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Solo.BLL.Migrations
{
    public partial class addManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FundManagerNo",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ManagerInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FundManagerNo = table.Column<string>(nullable: true),
                    FundManagerName = table.Column<string>(nullable: true),
                    FundCompanyNo = table.Column<string>(nullable: true),
                    FundCompanyName = table.Column<string>(nullable: true),
                    ManagerFundNos = table.Column<string>(nullable: true),
                    ManagerFundNames = table.Column<string>(nullable: true),
                    DutyTime = table.Column<int>(nullable: false),
                    MainFundNo = table.Column<string>(nullable: true),
                    MainFundName = table.Column<string>(nullable: true),
                    ManagerScale = table.Column<double>(nullable: false),
                    BestReturn = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagerInfos");

            migrationBuilder.DropColumn(
                name: "FundManagerNo",
                table: "FundInfos");
        }
    }
}
