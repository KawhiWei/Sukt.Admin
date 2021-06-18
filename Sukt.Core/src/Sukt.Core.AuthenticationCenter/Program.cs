using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using AspectCore.Extensions.Hosting;
namespace Sukt.Core.AuthenticationCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.UseServiceContext()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}