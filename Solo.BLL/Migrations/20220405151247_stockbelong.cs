using Microsoft.EntityFrameworkCore.Migrations;

namespace Solo.BLL.Migrations
{
    public partial class stockbelong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HS300",
                table: "StockInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SZ50",
                table: "StockInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ZZ1000",
                table: "StockInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ZZ500",
                table: "StockInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<float>(
                name: "Volatility",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "ValuationId",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Stddev3",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Stddev2",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Stddev",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Sharp3",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Sharp2",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Sharp",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "MorningStar",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Maxretra5",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Maxretra",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Estimate",
                table: "FundInfos",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HS300",
                table: "StockInfos");

            migrationBuilder.DropColumn(
                name: "SZ50",
                table: "StockInfos");

            migrationBuilder.DropColumn(
                name: "ZZ1000",
                table: "StockInfos");

            migrationBuilder.DropColumn(
                name: "ZZ500",
                table: "StockInfos");

            migrationBuilder.AlterColumn<double>(
                name: "Volatility",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ValuationId",
                table: "FundInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Stddev3",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Stddev2",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Stddev",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Sharp3",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Sharp2",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Sharp",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MorningStar",
                table: "FundInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Maxretra5",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Maxretra",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Estimate",
                table: "FundInfos",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
