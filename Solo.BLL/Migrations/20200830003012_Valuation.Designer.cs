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
    [Migration("20200830003012_Valuation")]
    partial class Valuation
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

                    b.Property<float?>("ReturnRate")
                        .HasColumnType("float");

                    b.Property<float?>("SaleFee")
                        .HasColumnType("float");

                    b.Property<float?>("StockRate")
                        .HasColumnType("float");

                    b.Property<float?>("TotalScale")
                        .HasColumnType("float");

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

            modelBuilder.Entity("Solo.Model.ViewModel.MyFund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Estimate")
                        .HasColumnType("double");

                    b.Property<string>("FundNo")
                        .HasColumnType("text");

                    b.Property<double?>("InNetValue")
                        .HasColumnType("double");

                    b.Property<int>("Investment")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Strategy")
                        .HasColumnType("text");

                    b.Property<int>("ValuationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MyFunds");
                });
#pragma warning restore 612, 618
        }
    }
}
