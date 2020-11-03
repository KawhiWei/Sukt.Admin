using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;

namespace Sukt.Core.Shared.Modules
{
    public class StartupModuleRunner : ModuleApplicationBase, IStartupModuleRunner
    {
        /// <summary>
        /// 程序启动运行时
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <param name="services"></param>
        public StartupModuleRunner(Type startupModuleType, IServiceCollection services)
            : base(startupModuleType, services)
        {
            services.AddSingleton<IStartupModuleRunner>(this);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            SuktIocManage.Instance.SetServiceCollection(services);
            var context = new ConfigureServicesContext(services);
            services.AddSingleton(context);
            foreach (var contextcfg in Modules)
            {
                services.AddSingleton(contextcfg);
                contextcfg.ConfigureServices(context);
            }
        }

        public void Initialize(IServiceProvider service)
        {
            SuktIocManage.Instance.SetApplicationServiceProvider(service);
            SetServiceProvider(service);
            using var scope = ServiceProvider.CreateScope();
            //using var scope = service.CreateScope();
            var ctx = new ApplicationContext(scope.ServiceProvider);
            foreach (var cfg in Modules)
            {
                cfg.ApplicationInitialization(ctx);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            if (ServiceProvider is IDisposable disposableServiceProvider)
            {
                disposableServiceProvider.Dispose();
            }
        }
    }
}