using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;

namespace Sukt.Core.Shared.Modules
{
    public static class AppModuleExtensions
    {
        public static IServiceCollection AddApplication<T>(this IServiceCollection services) where T : ISuktAppModule
        {
            services.AddApplication(typeof(T));
            return services;
        }

        private static IServiceCollection AddApplication(this IServiceCollection services, Type type)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var obj = new ObjectAccessor<IApplicationBuilder>();
            services.Add(ServiceDescriptor.Singleton(typeof(ObjectAccessor<IApplicationBuilder>), obj));
            services.Add(ServiceDescriptor.Singleton(typeof(IObjectAccessor<IApplicationBuilder>), obj));
            IStartupModuleRunner runner = new StartupModuleRunner(type, services);
            runner.ConfigureServices(services);
            return services;
        }

        public static IApplicationBuilder InitializeApplication(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = builder;
            var runner = builder.ApplicationServices.GetRequiredService<IStartupModuleRunner>();
            runner.Initialize(builder.ApplicationServices);
            return builder;
        }
    }
}