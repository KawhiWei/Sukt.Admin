using AspectCore.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Sukt.Core.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()

            //    .MinimumLevel.Information()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console()
            //    .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
            //SeriLogLogger.SetSeriLoggerToFile("logs");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.UseServiceContext()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //如果API项目需要接入GRPC服务需要配置两个Kestrel主机，分别指定两个不通端口，因为GRPC默认是使用https 
                    //webBuilder.ConfigureKestrel(opt =>
                    //{
                    //    opt.ListenLocalhost(8852, o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1);
                    //    opt.ListenLocalhost(9852, o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
                    //});
                    webBuilder.UseStartup<Startup>()
                    //                    .ConfigureKestrel(options =>
                    //                    {

                    //#if DEBUG

                    //                        options.ListenLocalhost(8361, o => o.Protocols =
                    //                            HttpProtocols.Http2);

                    //                        // ADDED THIS LINE to fix the problem
                    //                        options.ListenLocalhost(8001, o => o.Protocols =
                    //                            HttpProtocols.Http1);
                    //#else

                    //                        // ADDED THIS LINE to fix the problem
                    //                        options.ListenAnyIP(80, o => o.Protocols =
                    //                            HttpProtocols.Http1);
                    //                        options.ListenAnyIP(8331, o => o.Protocols =
                    //                                                    HttpProtocols.Http2);

                    //#endif
                    //                    })
                    .UseSerilog((webHost, configuration) =>
                    {

                        //得到配置文件
                        var serilog = webHost.Configuration.GetSection("Serilog");
                        //最小级别
                        var minimumLevel = serilog["MinimumLevel:Default"];
                        //日志事件级别
                        var logEventLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLevel);


                        configuration.ReadFrom.
                        Configuration(webHost.Configuration.GetSection("Serilog")).Enrich.FromLogContext().WriteTo.Console(logEventLevel);
                        configuration.WriteTo.Map(le => MapData(le),
                (key, log) => log.Async(o => o.File(Path.Combine("logs", @$"{key.time:yyyy-MM-dd}\{key.level.ToString().ToLower()}.txt"), logEventLevel)));

                        (DateTime time, LogEventLevel level) MapData(LogEvent logEvent)
                        {

                            return (new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, logEvent.Timestamp.Second), logEvent.Level);
                        }

                    })//注入Serilog日志中间件//这里是配置log的
                    .ConfigureLogging((hostingContext, builder) =>
                    {
                        builder.ClearProviders();
                        builder.SetMinimumLevel(LogLevel.Information);
                        builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        builder.AddConsole();
                        builder.AddDebug();
                    });
                })
            .UseDynamicProxy();//使用动态代理需要在Program引用此方法
    }
}
