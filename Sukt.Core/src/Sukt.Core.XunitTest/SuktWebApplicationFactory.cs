using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktAppModules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.XunitTest
{
    /// <summary>
    /// 单元测试工厂
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    public class SuktWebApplicationFactory<TStartup>: WebApplicationFactory<TStartup> where TStartup:class
    {
        public IServiceCollection IServiceCollection { get; private set; }
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var bulild = new WebHostBuilder().UseStartup<SuktTestStartup>();
            return bulild;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IAssemblyFinder, AssemblyFinder>();
                services.AddSingleton<ISuktAppModuleManager, SuktAppModuleManager>();
                var provider = services.BuildServiceProvider();
                var appModuleManager = provider.GetService<ISuktAppModuleManager>();
                appModuleManager.LoadModules(services);
                IServiceCollection = services;
            });
        }

    }
}
