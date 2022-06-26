using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Solo.BLL.Migrations
{
    public partial class addDailyRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    My = table.Column<double>(nullable: false),
                    XYG = table.Column<double>(nullable: false),
                    KJ = table.Column<double>(nullable: false),
                    Alex_My = table.Column<double>(nullable: false),
                    Alex = table.Column<double>(nullable: false),
                    HS300 = table.Column<double>(nullable: false),
                    CYB = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyRates");
        }
    }
}
