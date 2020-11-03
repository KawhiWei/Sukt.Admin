using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sukt.Core.Shared.AppOption;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <typeparam name="T">非静态强类型</typeparam>
        /// <returns>日志对象</returns>
        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        public static AppOptionSettings GetAppSettings(this IServiceProvider provider)
        {
            provider.NotNull(nameof(provider));
            return provider.GetService<IOptions<AppOptionSettings>>()?.Value;
        }

        public static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
            }

            return descriptor.ImplementationFactory(provider);
        }

        public static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
        }

        public static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
        {
            return ActivatorUtilities.CreateInstance(provider, type, arguments);
        }

        public static void GetService<T>(this IServiceProvider provider, Action<T> action)
        {
            action.NotNull(nameof(action));
            var t = provider.GetService<T>();
            action(t);
        }

        /// <summary>
        ///创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static void CreateScoped<T, TS>(this IServiceProvider provider, Action<TS, T> callback)
        {
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();

            callback(service, scope.ServiceProvider.GetRequiredService<T>());
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static void CreateScoped<TS>(this IServiceProvider provider, Action<TS> callback)
        {
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            callback(service);
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static T CreateScoped<T, TS>(this IServiceProvider provider, Func<TS, T> callback)
        {
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            return callback(service);
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static async Task CreateScopedAsync<T, TS>(this IServiceProvider provider, Func<TS, T, Task> callback)
        {
            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();

            await callback(service, scope.ServiceProvider.GetRequiredService<T>());
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static async Task CreateScopedAsync<TS>(this IServiceProvider provider, Func<TS, Task> callback)
        {
            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            await callback(service);
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        ///创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static async Task<T> CreateScopedAsync<T, TS>(this IServiceProvider provider, Func<TS, Task<T>> callback)
        {
            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            return await callback(service);
        }

        public static void CreateScoped(this IServiceProvider provider, Action<IServiceProvider> callback)
        {
            using var scope = provider.CreateScope();
            callback(scope.ServiceProvider);
        }
    }
}