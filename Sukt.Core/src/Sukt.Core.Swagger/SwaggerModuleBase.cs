using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;

namespace Sukt.Core.Swagger
{
    public class SwaggerModule : SuktAppModule
    {
        private string _url = string.Empty;
        private string _title = string.Empty;
        private string _version = string.Empty;

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            IConfiguration configuration = context.Services.GetConfiguration();
            //IConfiguration configuration = service.GetConfiguration();
            var title = configuration["SuktCore:Swagger:Title"];
            var version = configuration["SuktCore:Swagger:Version"];
            var url = configuration["SuktCore:Swagger:Url"];
            if (url.IsNullOrEmpty())
            {
                throw new SuktAppException("Url不能为空 ！！！");
            }

            if (version.IsNullOrEmpty())
            {
                throw new SuktAppException("版本号不能为空 ！！！");
            }

            if (title.IsNullOrEmpty())
            {
                throw new SuktAppException("标题不能为空 ！！！");
            }
            _title = title;
            _url = url;
            _version = version;
            context.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var files = Directory.GetFiles(basePath, "*.xml");
                foreach (var fiel in files)
                {
                    x.IncludeXmlComments(fiel, true);
                }
                x.CustomOperationIds(api =>
                {
                    return api.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });
                // 一定要返回true！
                x.DocInclusionPredicate((docName, description) =>
                {
                    return true;
                });
                ////https://github.com/domaindrivendev/Swashbuckle.AspNetCore
                //s.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //s.OperationFilter<SecurityRequirementsOperationFilter>();  // 很重要！这里配置安全校验，和之前的版本不一样
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="oauth2"}},
                        new []{ "readAccess", "writeAccess" }
                    }
                });
            });
            //return service;
        }

        public override void ApplicationInitialization(ApplicationContext context)
        {
            var applicationBuilder = context.GetApplicationBuilder();
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwagger();
            var template = $"doc/" + "BasicsService" + "/{documentName}/swagger.json";
            applicationBuilder.UseSwagger(c =>
            {
                c.RouteTemplate = template;
            });
            applicationBuilder.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(_url, $"{_version}");
                x.RoutePrefix = string.Empty;
            });
        }
    }
}