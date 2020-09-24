using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solo.BLL;

namespace ManagerWeb
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string DbType = AppConfigurtaionServices.Configuration.GetSection("DbType").Value;
            if (DbType == "MySql")
            {
                services.AddSingleton<IFundInfoService, FundInfoMySqlService>();
            }
            else if (DbType == "MsSql")
            {
                services.AddSingleton<IFundInfoService, FundInfoMsSqlService>();
            }
            //���cors ���� ���ÿ�����   
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                    //.AllowCredentials()//ָ������cookie
                .AllowAnyOrigin().AllowAnyHeader(); //�����κ���Դ����������
                });
            });
            services.AddSingleton<IJobService, JobService>();
            services.AddSingleton<ITaskInfoService, TaskInfoEFService>();
            services.AddSingleton<IUserInfoService, UserInfoService>();            
            services.AddSingleton<IMyFundService, MyFundService>(); 
            services.AddSingleton<ITransactionInfoService, TransactionInfoService>();
            services.AddSingleton<IHolidayService, HolidayService>();
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o => {
                o.Cookie.Name = "UserName.Cookies";
                o.LoginPath = new PathString("/User/LoginPage");
                o.AccessDeniedPath = new PathString("/Home/AccessDeny");
               //o.LogoutPath = new PathString("/User/Logout");
            });
            services.AddAuthorization(config =>
            {
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //    .RequireAuthenticatedUser()
                //    .RequireClaim(ClaimTypes.DateOfBirth)
                //    .Build();

                //config.DefaultPolicy = defaultAuthPolicy;


                //config.AddPolicy("Claim.DoB", policyBuilder =>
                //{
                //    policyBuilder.RequireClaim(ClaimTypes.DateOfBirth);
                //});

                config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin"));

            });
            //services.AddMvc(option =>
            //{
            //    option.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            ////��������������Դ����
            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //});

            // who are you?
            app.UseAuthentication();

            // are you allowed?

            //ע��app.UseAuthentication����һ��Ҫ���������app.UseMvc����ǰ�棬���ߺ���������HttpContext.SignInAsync�����û���¼��ʹ��
            //HttpContext.User���ǻ���ʾ�û�û�е�¼������HttpContext.User.Claims��ȡ������¼�û����κ���Ϣ��
            //��˵��Asp.Net OWIN�����MiddleWare�ĵ���˳����ϵͳ���ܲ����ܴ��Ӱ�죬����MiddleWare�ĵ���˳��һ�����ܷ�
            app.UseAuthorization();

            //����Cors
            app.UseCors("any");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Fund}/{action=wind}/{id?}");
            });
        }
    }
}
