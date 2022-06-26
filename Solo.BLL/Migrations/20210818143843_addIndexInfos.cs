using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Solo.BLL.Migrations
{
    public partial class addIndexInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndexInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IndexCode = table.Column<string>(nullable: true),
                    IndexName = table.Column<string>(nullable: true),
                    IndexValua = table.Column<int>(nullable: false),
                    PE = table.Column<double>(nullable: false),
                    PEP100 = table.Column<double>(nullable: false),
                    PB = table.Column<double>(nullable: false),
                    PBP100 = table.Column<double>(nullable: false),
                    GXL = table.Column<double>(nullable: false),
                    ROE = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexInfos");
        }
    }
}
