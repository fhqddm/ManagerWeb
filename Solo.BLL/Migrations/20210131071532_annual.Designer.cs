﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solo.BLL;

namespace Solo.BLL.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210131071532_annual")]
    partial class annual
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Solo.Model.DailyRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Alex")
                        .HasColumnType("double");

                    b.Property<double>("Alex_My")
                        .HasColumnType("double");

                    b.Property<double>("CYB")
                        .HasColumnType("double");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<double>("HS300")
                        .HasColumnType("double");

                    b.Property<double>("KJ")
                        .HasColumnType("double");

                    b.Property<double>("My")
                        .HasColumnType("double");

                    b.Property<double>("WYBCG")
                        .HasColumnType("double");

                    b.Property<double>("XYG")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("DailyRates");
                });

            modelBuilder.Entity("Solo.Model.FundDailyIDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataTime")
                        .HasColumnType("datetime");

                    b.Property<float?>("EquityReturn")
                        .HasColumnType("float");

                    b.Property<string>("FundNo")
                        .HasColumnType("text");

                    b.Property<float?>("MA10")
                        .HasColumnType("float");

                    b.Property<float?>("MA20")
                        .HasColumnType("float");

                    b.Property<float?>("MA5")
                        .HasColumnType("float");

                    b.Property<float?>("MA60")
                        .HasColumnType("float");

                    b.Property<float?>("NetValue")
                        .HasColumnType("float");

                    b.Property<float?>("PureNet")
                        .HasColumnType("float");

                    b.Property<float?>("Seperate")
                        .HasColumnType("float");

                    b.Property<float?>("Slope10")
                        .HasColumnType("float");

                    b.Property<float?>("Slope20")
                        .HasColumnType("float");

                    b.Property<float?>("Slope60")
                        .HasColumnType("float");

                    b.Property<float?>("Trend10")
                        .HasColumnType("float");

                    b.Property<float?>("Trend20")
                        .HasColumnType("float");

                    b.Property<float?>("Trend60")
                        .HasColumnType("float");

                    b.Property<float?>("UnitMoney")
                        .HasColumnType("float");

                    b.Property<float?>("UnitValue")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FundDailyIDetails");
                });

            modelBuilder.Entity("Solo.Model.FundInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float?>("A2015")
                        .HasColumnType("float");

                    b.Property<float?>("A2016")
                        .HasColumnType("float");

                    b.Property<float?>("A2017")
                        .HasColumnType("float");

                    b.Property<float?>("A2018")
                        .HasColumnType("float");

                    b.Property<float?>("A2019")
                        .HasColumnType("float");

                    b.Property<float?>("A2020")
                        .HasColumnType("float");

                    b.Property<float?>("A2021")
                        .HasColumnType("float");

                    b.Property<float?>("BuyRate")
                        .HasColumnType("float");

                    b.Property<string>("Combines")
                        .HasColumnType("text");

                    b.Property<float?>("CustodyFee")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DutyDate")
                        .HasColumnType("datetime");

                    b.Property<double>("Estimate")
                        .HasColumnType("double");

                    b.Property<string>("FundManagerNo")
                        .HasColumnType("text");

                    b.Property<string>("FundName")
                        .HasColumnType("text");

                    b.Property<string>("FundNo")
                        .HasColumnType("text");

                    b.Property<float?>("FundScore")
                        .HasColumnType("float");

                    b.Property<string>("FundType")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FundUpdateTime")
                        .HasColumnType("datetime");

                    b.Property<float?>("ManagerFee")
                        .HasColumnType("float");

                    b.Property<float?>("ManagerScore")
                        .HasColumnType("float");

                    b.Property<double>("Maxretra")
                        .HasColumnType("double");

                    b.Property<int>("MorningStar")
                        .HasColumnType("int");

                    b.Property<float?>("NetValue")
                        .HasColumnType("float");

                    b.Property<float?>("OrgHoldRate")
                        .HasColumnType("float");

                    b.Property<float?>("Over1yFee")
                        .HasColumnType("float");

                    b.Property<float?>("Over2yFee")
                        .HasColumnType("float");

                    b.Property<float?>("Over30dFee")
                        .HasColumnType("float");

                    b.Property<float?>("Over7dFee")
                        .HasColumnType("float");

                    b.Property<float?>("R1day")
                        .HasColumnType("float");

                    b.Property<float?>("R1month")
                        .HasColumnType("float");

                    b.Property<float?>("R1week")
                        .HasColumnType("float");

                    b.Property<float?>("R1year")
                        .HasColumnType("float");

                    b.Property<float?>("R2015")
                        .HasColumnType("float");

                    b.Property<float?>("R2016")
                        .HasColumnType("float");

                    b.Property<float?>("R2017")
                        .HasColumnType("float");

                    b.Property<float?>("R2018")
                        .HasColumnType("float");

                    b.Property<float?>("R2019")
                        .HasColumnType("float");

                    b.Property<float?>("R2020")
                        .HasColumnType("float");

                    b.Property<float?>("R2021")
                        .HasColumnType("float");

                    b.Property<float?>("R2year")
                        .HasColumnType("float");

                    b.Property<float?>("R3month")
                        .HasColumnType("float");

                    b.Property<float?>("R3year")
                        .HasColumnType("float");

                    b.Property<float?>("R5year")
                        .HasColumnType("float");

                    b.Property<float?>("R6month")
                        .HasColumnType("float");

                    b.Property<float?>("SaleFee")
                        .HasColumnType("float");

                    b.Property<double>("Sharp")
                        .HasColumnType("double");

                    b.Property<double>("Sharp2")
                        .HasColumnType("double");

                    b.Property<double>("Sharp3")
                        .HasColumnType("double");

                    b.Property<double>("Stddev")
                        .HasColumnType("double");

                    b.Property<double>("Stddev2")
                        .HasColumnType("double");

                    b.Property<double>("Stddev3")
                        .HasColumnType("double");

                    b.Property<string>("StockNames")
                        .HasColumnType("text");

                    b.Property<string>("StockNos")
                        .HasColumnType("text");

                    b.Property<string>("StockPositions")
                        .HasColumnType("text");

                    b.Property<float?>("StockRate")
                        .HasColumnType("float");

                    b.Property<string>("Strategy")
                        .HasColumnType("text");

                    b.Property<float?>("TotalScale")
                        .HasColumnType("float");

                    b.Property<double>("TotalUnitMoney")
                        .HasColumnType("double");

                    b.Property<string>("Turnovers")
                        .HasColumnType("text");

                    b.Property<int>("ValuationId")
                        .HasColumnType("int");

                    b.Property<double>("Volatility")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("FundInfos");
                });

            modelBuilder.Entity("Solo.Model.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("HolidayType")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("Solo.Model.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Requirement")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Solo.Model.ManagerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("BestReturn")
                        .HasColumnType("double");

                    b.Property<int>("DutyTime")
                        .HasColumnType("int");

                    b.Property<string>("FundCompanyName")
                        .HasColumnType("text");

                    b.Property<string>("FundCompanyNo")
                        .HasColumnType("text");

                    b.Property<string>("FundManagerName")
                        .HasColumnType("text");

                    b.Property<string>("FundManagerNo")
                        .HasColumnType("text");

                    b.Property<string>("MainFundName")
                        .HasColumnType("text");

                    b.Property<string>("MainFundNo")
                        .HasColumnType("text");

                    b.Property<string>("ManagerFundNames")
                        .HasColumnType("text");

                    b.Property<string>("ManagerFundNos")
                        .HasColumnType("text");

                    b.Property<double>("ManagerScale")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("ManagerInfos");
                });

            modelBuilder.Entity("Solo.Model.MasterPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("ALEX_Position")
                        .HasColumnType("double");

                    b.Property<string>("FundNo")
                        .HasColumnType("text");

                    b.Property<double>("KJ_Position")
                        .HasColumnType("double");

                    b.Property<double>("XYG_Position")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("MasterPositions");
                });

            modelBuilder.Entity("Solo.Model.MyStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("StockName")
                        .HasColumnType("text");

                    b.Property<string>("StockNo")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MyStocks");
                });

            modelBuilder.Entity("Solo.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Solo.Model.StockInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("FundTop10HoldRate")
                        .HasColumnType("double");

                    b.Property<double>("GrossProfit")
                        .HasColumnType("double");

                    b.Property<string>("IndustryId")
                        .HasColumnType("text");

                    b.Property<string>("IndustryName")
                        .HasColumnType("text");

                    b.Property<double>("Market")
                        .HasColumnType("double");

                    b.Property<double>("MarketCap")
                        .HasColumnType("double");

                    b.Property<double>("NetAssets")
                        .HasColumnType("double");

                    b.Property<double>("NetMargin")
                        .HasColumnType("double");

                    b.Property<double>("NetRate")
                        .HasColumnType("double");

                    b.Property<double>("P_B_Ratio")
                        .HasColumnType("double");

                    b.Property<double>("P_E_Ratio")
                        .HasColumnType("double");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<double>("R1day")
                        .HasColumnType("double");

                    b.Property<double>("ROE")
                        .HasColumnType("double");

                    b.Property<string>("StockName")
                        .HasColumnType("text");

                    b.Property<string>("StockNo")
                        .HasColumnType("text");

                    b.Property<DateTime>("StockUpdateTime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("StockInfos");
                });

            modelBuilder.Entity("Solo.Model.Suggest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("SubmitTime")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Suggests");
                });

            modelBuilder.Entity("Solo.Model.TaskInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime");

                    b.Property<string>("Detail")
                        .HasColumnType("text");

                    b.Property<double>("Duration")
                        .HasColumnType("double");

                    b.Property<string>("SkillName")
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("taskName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TaskInfos");
                });

            modelBuilder.Entity("Solo.Model.TransactionInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ConfirmTime")
                        .HasColumnType("datetime");

                    b.Property<string>("FundNo")
                        .HasColumnType("text");

                    b.Property<double>("TransactionValue")
                        .HasColumnType("double");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TransactionInfos");
                });

            modelBuilder.Entity("Solo.Model.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("EncryptedPassword")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Solo.Model.Valuation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("ValuationScore")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Valuations");
                });

            modelBuilder.Entity("Solo.Model.ViewModel.MyFund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FundNo")
                        .HasColumnType("text");

                    b.Property<int>("Investment")
                        .HasColumnType("int");

                    b.Property<float?>("ReturnRate")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MyFunds");
                });
#pragma warning restore 612, 618
        }
    }
}
