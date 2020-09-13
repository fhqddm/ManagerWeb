using Microsoft.EntityFrameworkCore;
using Solo.Model;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Solo.BLL
{
    public class MyContext: DbContext
    {
        public DbSet<TaskInfo> TaskInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<FundInfo> FundInfos { get; set; }
        public DbSet<MyFund> MyFunds { get; set; }
        public DbSet<TransactionInfo> TransactionInfos { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<FundDailyIDetail> FundDailyIDetails { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Valuation> Valuations { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string DbType = AppConfigurtaionServices.Configuration.GetSection("DbType").Value;

            if (DbType == "MySql")
            {
                string conStr = AppConfigurtaionServices.Configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySQL(conStr);
            }
            else if (DbType == "MsSql")
            {
                string conStr = AppConfigurtaionServices.Configuration.GetConnectionString("MsSqlConnection");
                optionsBuilder.UseSqlServer(conStr);
            }
            base.OnConfiguring(optionsBuilder);
        }

    }

    public class AppConfigurtaionServices
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }
    }
}
