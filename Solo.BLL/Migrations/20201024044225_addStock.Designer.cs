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
    [Migration("20201024044225_addStock")]
    partial class addStock
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.Property<float?>("NetValue")
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

                    b.Property<float?>("BuyRate")
                        .HasColumnType("float");

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

                    b.Property<DateTime?>("FundUpdateTime")
                        .HasColumnType("datetime");

                    b.Property<float?>("ManagerFee")
                        .HasColumnType("float");

                    b.Property<float?>("ManagerScore")
                        .HasColumnType("float");

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

                    b.Property<string>("StockNos")
                        .HasColumnType("text");

                    b.Property<float?>("StockRate")
                        .HasColumnType("float");

                    b.Property<string>("Strategy")
                        .HasColumnType("text");

                    b.Property<float?>("TotalScale")
                        .HasColumnType("float");

                    b.Property<int>("ValuationId")
                        .HasColumnType("int");

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

                    b.Property<double>("GrossProfit")
                        .HasColumnType("double");

                    b.Property<double>("Market")
                        .HasColumnType("double");

                    b.Property<double>("MarketCap")
                        .HasColumnType("double");

                    b.Property<double>("NetAssets")
                        .HasColumnType("double");

                    b.Property<double>("NetMargin")
                        .HasColumnType("double");

                    b.Property<double>("NetProfit")
                        .HasColumnType("double");

                    b.Property<double>("P_B_Ratio")
                        .HasColumnType("double");

                    b.Property<double>("P_E_Ratio")
                        .HasColumnType("double");

                    b.Property<double>("ROE")
                        .HasColumnType("double");

                    b.Property<string>("StockName")
                        .HasColumnType("text");

                    b.Property<string>("StockNo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StockInfos");
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
