using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sukt.Core.Shared.Extensions
{
    public interface IServiceModule
    {
        void ConfigureServices(IServiceCollection services);
    }
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services, params Assembly[] assemblies)
            => RegisterAssemblyTypes(services, null, ServiceLifetime.Singleton, assemblies);
        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="serviceLifetime">service lifetime</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services,
            ServiceLifetime serviceLifetime, params Assembly[] assemblies)
            => RegisterAssemblyTypes(services, null, serviceLifetime, assemblies);
        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="typesFilter">filter types to register</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services,
            Func<Type, bool> typesFilter, params Assembly[] assemblies)
            => RegisterAssemblyTypes(services, typesFilter, ServiceLifetime.Singleton, assemblies);
        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="typesFilter">filter types to register</param>
        /// <param name="serviceLifetime">service lifetime</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services, Func<Type, bool> typesFilter, ServiceLifetime serviceLifetime, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = ReflectHelper.GetAssemblies();
            }

            var types = assemblies
                .Select(assembly => assembly.GetExportedTypes())
                .SelectMany(t => t);
            if (typesFilter != null)
            {
                types = types.Where(typesFilter);
            }

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type, type, serviceLifetime));
            }

            return services;
        }
        /// <summary>
        /// RegisterTypeAsImplementedInterfaces
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypesAsImplementedInterfaces(this IServiceCollection services,
            params Assembly[] assemblies)
            => RegisterAssemblyTypesAsImplementedInterfaces(services, typesFilter: null, ServiceLifetime.Singleton, assemblies);
        /// <summary>
        /// RegisterTypeAsImplementedInterfaces
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="serviceLifetime">service lifetime</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypesAsImplementedInterfaces(this IServiceCollection services,
            ServiceLifetime serviceLifetime, params Assembly[] assemblies)
            => RegisterAssemblyTypesAsImplementedInterfaces(services, typesFilter: null, serviceLifetime, assemblies);
        /// <summary>
        /// RegisterTypeAsImplementedInterfaces, singleton by default
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="typesFilter">filter types to register</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypesAsImplementedInterfaces(this IServiceCollection services, Func<Type, bool> typesFilter, params Assembly[] assemblies)
            => RegisterAssemblyTypesAsImplementedInterfaces(services, typesFilter: typesFilter, ServiceLifetime.Singleton, assemblies);

        /// <summary>
        /// RegisterTypeAsImplementedInterfaces
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="typesFilter">filter types to register</param>
        /// <param name="serviceLifetime">service lifetime</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyTypesAsImplementedInterfaces(this IServiceCollection services, Func<Type, bool> typesFilter, ServiceLifetime serviceLifetime, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = ReflectHelper.GetAssemblies();
            }

            var types = assemblies
                .Select(assembly => assembly.GetExportedTypes())
                .SelectMany(t => t);
            if (typesFilter != null)
            {
                types = types.Where(typesFilter);
            }

            foreach (var type in types)
            {
                foreach (var implementedInterface in type.GetImplementedInterfaces())
                {
                    services.Add(new ServiceDescriptor(implementedInterface, type, serviceLifetime));
                }
            }

            return services;
        }
        /// <summary>
        /// RegisterTypeAsImplementedInterfaces
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="type">type</param>
        /// <param name="serviceLifetime">service lifetime</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterTypeAsImplementedInterfaces(this IServiceCollection services, Type type, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            if (type != null)
            {

                foreach (var interfaceType in type.GetImplementedInterfaces())
                {
                    services.Add(new ServiceDescriptor(interfaceType, type, serviceLifetime));
                }
            }
            return services;
        }
        /// <summary>
        /// RegisterAssemblyModules
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="assemblies">assemblies</param>
        /// <returns>services</returns>
        public static IServiceCollection RegisterAssemblyModules(
            [NotNull] this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = ReflectHelper.GetAssemblies();
            }
            foreach (var type in assemblies.SelectMany(ass => ass.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IServiceModule).IsAssignableFrom(t))
            )
            {
                try
                {
                    if (Activator.CreateInstance(type) is IServiceModule module)
                    {
                        module.ConfigureServices(services);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return services;
        }
        /// <summary>
        /// 得到注入服务
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TType GetService<TType>(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            return provider.GetService<TType>();
        }
        /// <summary>
        /// 得到或添加Singleton服务
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TType GetOrAddSingletonService<TType, TImplementation>(this IServiceCollection services) where TType : class
       where TImplementation : class, TType
        {
            var type = services.GetService<TType>();
            if (type is null)
            {
                services.AddSingleton<TType, TImplementation>();
                type = services.GetService<TType>();
            }

            return type;
        }

        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return services.GetSingletonInstanceOrNull<IConfiguration>();
        }

        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            ServiceDescriptor descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Singleton);

            if (descriptor?.ImplementationInstance != null)
            {
                return (T)descriptor.ImplementationInstance;
            }

            if (descriptor?.ImplementationFactory != null)
            {
                return (T)descriptor.ImplementationFactory.Invoke(null);
            }

            return default;
        }

    }
}
