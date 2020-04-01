using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktAppModules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块管理
        /// </summary>
        /// <typeparam name="TAppModuleManager"></typeparam>
        /// <param name="services"></param>
        public static IServiceCollection AddAppModuleManager<TAppModuleManager>(this IServiceCollection services)
            where TAppModuleManager : ISuktAppModuleManager, new()
        {
            services.NotNull(nameof(services));
            services.AddSingleton<IAssemblyFinder, AssemblyFinder>();
            TAppModuleManager module = new TAppModuleManager();
            services.AddSingleton<ISuktAppModuleManager>(module);
            module.LoadModules(services);
            return services;
        }
    }
}
