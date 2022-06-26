using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using ManagerApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Solo.BLL;

namespace ManagerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TokenManagement>(Configuration.GetSection("tokenConfig"));
            var token = Configuration.GetSection("tokenConfig").Get<TokenManagement>();


            string DbType = AppConfigurtaionServices.Configuration.GetSection("DbType").Value;

            //���cors ���� ���ÿ�����   
            services.AddCors(options =>
            {
                //options.AddPolicy("any", builder =>
                //{
                //    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                //    //.AllowCredentials()//ָ������cookie
                //.AllowAnyOrigin().AllowAnyHeader(); //�����κ���Դ����������
                //});

                options.AddPolicy("Cors",builder =>
                {
                        builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                        .WithOrigins("http://www.zodiacn.ltd", "http://www.zodiacn.ltd:3333", "http://15398.cn", "http://15398.cn:3333", "http://192.168.31.24:8080", "http://192.168.31.24:3333", "http://localhost:8080",
                        "http://218.244.151.177", "http://218.244.151.177:8080", "http://218.244.151.177:3333", "http://192.168.31.135:8080", "http://192.168.31.135:3333", "http://192.168.1.6:8080", "http://43.154.99.239")
                        .AllowAnyHeader();
                });
            });

            if (DbType == "MySql")
            {
                services.AddSingleton<IFundInfoService, FundInfoMySqlService>();
            }
            else if (DbType == "MsSql")
            {
                services.AddSingleton<IFundInfoService, FundInfoMsSqlService>();
            }
            services.AddSingleton<IJobService, JobService>();
            services.AddSingleton<ITaskInfoService, TaskInfoEFService>();
            services.AddSingleton<IUserInfoService, UserInfoService>();
            services.AddSingleton<IMyFundService, MyFundService>();
            services.AddSingleton<IMyStockService, MyStockService>();
            services.AddSingleton<ITransactionInfoService, TransactionInfoService>();
            services.AddSingleton<IHolidayService, HolidayService>();
            services.AddSingleton<IStockInfoService, StockInfoService>();
            services.AddSingleton<ISuggestService, SuggestService>();
            services.AddControllers();


            #region JWT
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //Token Validation Parameters
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,//����token,��̨��֤token�Ƿ���Ч(��Ҫ)
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��

                    ValidateIssuerSigningKey = true,
                    //��ȡ������Ҫʹ�õ�Microsoft.IdentityModel.Tokens.SecurityKey����ǩ����֤��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                    GetBytes(token.Secret)),
                    //��ȡ������һ��System.String������ʾ��ʹ�õ���Ч�����߼����ҵķ����ߡ�
                    ValidIssuer = token.Issuer,
                    //��ȡ������һ���ַ��������ַ�����ʾ�����ڼ�����Ч���ڷ������ƵĹ��ڡ�
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
                x.Events = new JwtBearerEvents
                {
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json; charset=utf-8";
                        var message = "OnForbidden.";
                        Console.WriteLine("---------����OnForbidden--------");
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        context.Response.Headers.Add("Cache-Control", "no-cache");
                        return context.Response.WriteAsync(message);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json; charset=utf-8";
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        context.Response.Headers.Add("Cache-Control", "no-cache");
                        var message = "OnAuthenticationFailed.";
                        Console.WriteLine("---------����OnAuthenticationFailed--------");
                        return context.Response.WriteAsync(message);
                    },
                    //�˴�ΪȨ����֤ʧ�ܺ󴥷����¼�
                    OnChallenge = context =>
                    {
                        //�˴�����Ϊ��ֹ.Net CoreĬ�ϵķ������ͺ����ݽ�����������ҪŶ������
                        context.HandleResponse();
                        //�Զ����Լ���Ҫ���ص����ݽ����������Ҫ���ص���Json����ͨ������Newtonsoft.Json�����ת��
                        var payload = "�����֤ʧ��";
                        //�Զ��巵�ص���������
                        //context.Response.ContentType = "application/json";
                        //�Զ��巵��״̬�룬Ĭ��Ϊ401 ������ĳ� 200
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //���Json���ݽ��
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };

            });
            #endregion

            services.AddAuthorization(config =>
            {
                config.AddPolicy("RequireName", policyBuilder => policyBuilder.RequireUserName("zs"));
                config.AddPolicy("RequireRoles", policyBuilder => policyBuilder.RequireRole("Admin"));
                config.AddPolicy("RequireLogin", policyBuilder => policyBuilder.RequireRole("Guest", "Admin"));
            });
            


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


            ////Add User session
            //app.UseSession();

            ////Add JWToken to all incoming HTTP Request Header
            //app.Use(async (context, next) =>
            //{
            //    var JWToken = context.Session.GetString("JWToken");
            //    if (!string.IsNullOrEmpty(JWToken))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //    }
            //    await next();
            //});

            // who are you?
            app.UseAuthentication();

            // are you allowed?

            //ע��app.UseAuthentication����һ��Ҫ���������app.UseMvc����ǰ�棬���ߺ���������HttpContext.SignInAsync�����û���¼��ʹ��
            //HttpContext.User���ǻ���ʾ�û�û�е�¼������HttpContext.User.Claims��ȡ������¼�û����κ���Ϣ��
            //��˵��Asp.Net OWIN�����MiddleWare�ĵ���˳����ϵͳ���ܲ����ܴ��Ӱ�죬����MiddleWare�ĵ���˳��һ�����ܷ�
            app.UseAuthorization();

            //����Cors
            app.UseCors("Cors");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Fund}/{action=Strategy}/{id?}");
            });
        }
    }
}
