using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Modules;
using Sukt.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sukt.Core.API.Startups
{
    public class SuktSwaggerModule: SwaggerModule
    {
        private string _url = string.Empty;
        private string _title = string.Empty;
        private string _version = string.Empty;
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            IConfiguration configuration = context.Services.GetConfiguration();
            string title = configuration["SuktCore:Swagger:Title"];
            string version = configuration["SuktCore:Swagger:Version"];
            string text = configuration["SuktCore:Swagger:Url"];
            if (text.IsNullOrEmpty())
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
            _url = text;
            _version = version;
            context.Services.AddSwaggerGen(delegate (SwaggerGenOptions x)
            {
                x.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = _title,
                    Version = version
                });
                string[] files = Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml");
                foreach (string filePath in files)
                {
                    x.IncludeXmlComments(filePath, includeControllerXmlComments: true);
                }

                x.CustomOperationIds((ApiDescription api) => (!api.TryGetMethodInfo(out MethodInfo methodInfo)) ? null : methodInfo.Name);
                x.DocInclusionPredicate((string docName, ApiDescription description) => true);
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new string[2]
                        {
                            "readAccess",
                            "writeAccess"
                        }
                    }
                });
            });
        }
        public override void ApplicationInitialization(ApplicationContext context)
        {
            IApplicationBuilder applicationBuilder = context.GetApplicationBuilder();
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwagger();
            string template = "doc/AdminService/{documentName}/swagger.json";
            applicationBuilder.UseSwagger(delegate (SwaggerOptions c)
            {
                c.RouteTemplate = template;
            });
            applicationBuilder.UseSwaggerUI(delegate (SwaggerUIOptions x)
            {
                x.SwaggerEndpoint(_url, _version ?? "");
                x.RoutePrefix = string.Empty;
            });
        }
    }
}
