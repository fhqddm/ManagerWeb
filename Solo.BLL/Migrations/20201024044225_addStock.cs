using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Solo.BLL.Migrations
{
    public partial class addStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StockNos",
                table: "FundInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StockNo = table.Column<string>(nullable: true),
                    StockName = table.Column<string>(nullable: true),
                    Market = table.Column<double>(nullable: false),
                    P_E_Ratio = table.Column<double>(nullable: false),
                    MarketCap = table.Column<double>(nullable: false),
                    P_B_Ratio = table.Column<double>(nullable: false),
                    ROE = table.Column<double>(nullable: false),
                    NetProfit = table.Column<double>(nullable: false),
                    GrossProfit = table.Column<double>(nullable: false),
                    NetMargin = table.Column<double>(nullable: false),
                    NetAssets = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockInfos");

            migrationBuilder.DropColumn(
                name: "StockNos",
                table: "FundInfos");
        }
    }
}
