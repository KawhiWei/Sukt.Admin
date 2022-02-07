
using Sukt.Admin.EntityFrameworkCore;
using Sukt.Identity.Domain;
using Sukt.Module.Core.AppOption;
using Sukt.Swagger;

namespace Sukt.Admin.Api
{
    [SuktDependsOn(
        typeof(DependencyAppModule),
        typeof(AdminEntityFrameworkCoreModule),
        typeof(SwaggerModule),
        typeof(MigrationModule),
        typeof(SuktIdentityModule)
    )]
    public class SuktAppWebModule : SuktAppModule
    {
        private string _corePolicyName = string.Empty;
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            service.AddControllers(x =>
            {
                x.SuppressAsyncSuffixInActionNames = false;
                //x.Filters.Add<PermissionAuthorizationFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            context.Services.AddFileProvider();
            var configuration = service.GetConfiguration();
            if (configuration == null)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                configuration = configurationBuilder.Build();
                context.Services.AddSingleton<IConfiguration>(configuration);
            }
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            context.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(basePath));
            service.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            AppOptionSettings option = new();
            if (configuration != null)
            {
                configuration.Bind("SuktCore", option);
                context.Services.AddObjectAccessor(option);
                context.Services.Configure<AppOptionSettings>(o =>
                {
                    o.AuditEnabled = option.AuditEnabled;
                    o.Auth = option.Auth;
                    o.Cors = option.Cors;
                    o.DbContexts = option.DbContexts;
                    o.Jwt = option.Jwt;
                });
            }
            var settings = service.GetAppSettings();
            if (!settings.Cors.PolicyName.IsNullOrEmpty() && !settings.Cors.Url.IsNullOrEmpty()) //添加跨域
            {
                _corePolicyName = settings.Cors.PolicyName;
                service.AddCors(c =>
                {
                    c.AddPolicy(settings.Cors.PolicyName, policy =>
                    {
                        policy.WithOrigins(settings.Cors.Url
                          .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        //policy.WithOrigins("http://localhost:5001")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();//允许cookie;
                    });
                });
            }
        }
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var applicationBuilder = context.GetApplicationBuilder();
            if (!_corePolicyName.IsNullOrEmpty())
            {
                applicationBuilder.UseCors(_corePolicyName); //添加跨域中间件
            }
            //applicationBuilder.UseRouting();
            //applicationBuilder.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
