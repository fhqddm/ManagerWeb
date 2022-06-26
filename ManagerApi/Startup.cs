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

            //添加cors 服务 配置跨域处理   
            services.AddCors(options =>
            {
                //options.AddPolicy("any", builder =>
                //{
                //    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                //    //.AllowCredentials()//指定处理cookie
                //.AllowAnyOrigin().AllowAnyHeader(); //允许任何来源的主机访问
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
                    SaveSigninToken = true,//保存token,后台验证token是否生效(重要)
                    ValidateLifetime = true,//是否验证失效时间

                    ValidateIssuerSigningKey = true,
                    //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                    GetBytes(token.Secret)),
                    //获取或设置一个System.String，它表示将使用的有效发行者检查代币的发行者。
                    ValidIssuer = token.Issuer,
                    //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众。
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
                        Console.WriteLine("---------进入OnForbidden--------");
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
                        Console.WriteLine("---------进入OnAuthenticationFailed--------");
                        return context.Response.WriteAsync(message);
                    },
                    //此处为权限验证失败后触发的事件
                    OnChallenge = context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                        context.HandleResponse();
                        //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                        var payload = "身份验证失败";
                        //自定义返回的数据类型
                        //context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //输出Json数据结果
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

            ////设置允许所有来源跨域
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

            //注意app.UseAuthentication方法一定要放在下面的app.UseMvc方法前面，否者后面就算调用HttpContext.SignInAsync进行用户登录后，使用
            //HttpContext.User还是会显示用户没有登录，并且HttpContext.User.Claims读取不到登录用户的任何信息。
            //这说明Asp.Net OWIN框架中MiddleWare的调用顺序会对系统功能产生很大的影响，各个MiddleWare的调用顺序一定不能反
            app.UseAuthorization();

            //配置Cors
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
