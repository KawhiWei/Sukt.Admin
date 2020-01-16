using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uwl.Common.Helper;
using Uwl.Data.Server.ScheduleServices;
using UwlAPI.Tools.StartJob;

namespace UwlAPI.Tools.StartupExtension
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void SwaggerConfigureExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "UwlAPI.Tools",
                    Description = "框架说明文档",
                    //TermsOfService = "None",
                    Contact = new OpenApiContact() { Name = "UwlAPI.Tools", Email = "UwlAPI.Tools@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                var basepath = AppDomain.CurrentDomain.BaseDirectory;
                var xmls = System.IO.Directory.GetFiles(basepath, "*.xml");
                foreach (var xml in xmls)
                {
                    x.IncludeXmlComments(xml,true);
                }
                //var IssuerName = Appsettings.app(new string[] { "JwtSettings", "Issuer" });//  Configuration.GetSection("JwtSettings"))["Issuer"];// 发行人
                //var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                x.OperationFilter<AddResponseHeadersFilter>();
                x.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // header中加token，传递到后台
                x.OperationFilter<SecurityRequirementsOperationFilter>();
                //x.AddSecurityRequirement(security);
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

            });
        }



        /// <summary>
        /// app
        /// </summary>
        public static void SwaggerAppConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Uwl.Core.API";
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "I_Sfront.Core API");
                c.RoutePrefix = string.Empty;
            });
        }
        /// <summary>
        /// 启动时自动将已启动的job添加到应用程序
        /// </summary>
        /// <param name="app"></param>
        public static void AutoJob(this IApplicationBuilder app)
        {
            //手动调用IoC获取实例的方式
            var scheduler = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IScheduleServer>();
            //将获取到的实例传给自动启动Job类
            new AutoStartJob(scheduler).AutoJob();
        }
    }
}
