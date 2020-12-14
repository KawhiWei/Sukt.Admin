using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Helpers;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sukt.Core.Shared.Extensions
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
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
        /// 得到注入服务
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TType GetBuildService<TType>(this IServiceCollection services)
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
        public static TServiceType GetOrAddSingletonService<TServiceType, TImplementation>(this IServiceCollection services)
            where TServiceType : class
        where TImplementation : class, TServiceType
        {
            var type = services.GetSingletonInstanceOrNull<TServiceType>();
            if (type is null)
            {
                var provider = services.BuildServiceProvider();
                var serviceType = (TServiceType)provider.GetInstance(new ServiceDescriptor(typeof(TServiceType), typeof(TImplementation), ServiceLifetime.Singleton));
                return serviceType;
            }

            return type;
        }

        /// <summary>
        /// 得到或添加Singleton服务
        /// </summary>
        /// <typeparam name="TServiceType"></typeparam>

        public static TServiceType GetOrAddSingletonService<TServiceType>(this IServiceCollection services, Func<TServiceType> factory) where TServiceType : class
        {
            var servciceType = services.GetSingletonInstanceOrNull<TServiceType>();
            if (servciceType is null)
            {
                servciceType = factory();
                services.AddSingleton<TServiceType>(servciceType);
            }

            return servciceType;
        }

        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return services.GetBuildService<IConfiguration>();
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

        /// <summary>
        /// 得到操作设置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>

        public static AppOptionSettings GetAppSettings(this IServiceCollection services)
        {
            services.NotNull(nameof(services));
            return services.GetBuildService<IOptions<AppOptionSettings>>()?.Value;
        }

        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("找不到singleton服务: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        #region New Module

        public static ObjectAccessor<T> TryAddObjectAccessor<T>(this IServiceCollection services)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                return services.GetSingletonInstance<ObjectAccessor<T>>();
            }

            return services.AddObjectAccessor<T>();
        }

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>());
        }

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, T obj)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>(obj));
        }

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, ObjectAccessor<T> accessor)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                throw new Exception("在类型“{typeof(T).AssemblyQualifiedName)}”之前注册了对象: ");
            }

            //Add to the beginning for fast retrieve
            services.Insert(0, ServiceDescriptor.Singleton(typeof(ObjectAccessor<T>), accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));

            return accessor;
        }

        public static T GetObjectOrNull<T>(this IServiceCollection services)
            where T : class
        {
            return services.GetSingletonInstanceOrNull<IObjectAccessor<T>>()?.Value;
        }

        public static T GetObject<T>(this IServiceCollection services)
            where T : class
        {
            return services.GetObjectOrNull<T>() ?? throw new Exception($"找不到的对象 {typeof(T).AssemblyQualifiedName} 服务。请确保您以前使用过AddObjectAccessor！");
        }

        public static IServiceProvider BuildServiceProviderFromFactory([NotNull] this IServiceCollection services)
        {
            foreach (var service in services)
            {
                var factoryInterface = service.ImplementationInstance?.GetType()
                    .GetTypeInfo()
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetTypeInfo().IsGenericType &&
                                         i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));

                if (factoryInterface == null)
                {
                    continue;
                }

                var containerBuilderType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider)typeof(Extensions)
                    .GetTypeInfo()
                    .GetMethods()
                    .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
                    .MakeGenericMethod(containerBuilderType)
                    .Invoke(null, new object[] { services, null });
            }

            return services.BuildServiceProvider();
        }

        #endregion New Module
        /// <summary>
        /// 得到文件容器
        /// </summary>
        /// <param name="services">服务接口</param>
        /// <param name="fileName">文件名+后缀名</param>
        /// <param name="fileNotExistsMsg">文件不存提示信息</param>
        /// <returns>返回文件中的文件</returns>
        public static string GetFileText(this IServiceCollection services, string fileName, string fileNotExistsMsg)
        {
            fileName.NotNullOrEmpty(nameof(fileName));
            var fileProvider = services.GetSingletonInstanceOrNull<IFileProvider>();

            if (fileProvider == null)
            {

                throw new SuktAppException("IFileProvider接口不存在");
            }


            var fileInfo = fileProvider.GetFileInfo(fileName);
            if (!fileInfo.Exists)
            {
                if (!fileNotExistsMsg.IsNullOrEmpty())
                {
                    throw new SuktAppException(fileNotExistsMsg);
                }

            }
            var text = ReadAllText(fileInfo);
            if (text.IsNullOrEmpty())
            {
                throw new SuktAppException("文件内容不存在");
            }
            return text;
        }

        /// <summary>
        /// 根据配置得到文件内容
        /// </summary>
        /// <param name="services">服务接口</param>
        /// <param name=""></param>
        /// <param name="sectionKey">分区键</param>
        /// <param name="fileNotExistsMsg">文件不存提示信息</param>
        /// <returns>返回文件中的文件</returns>
        public static string GetFileByConfiguration(this IServiceCollection services, string sectionKey, string fileNotExistsMsg)
        {


            sectionKey.NotNullOrEmpty(nameof(sectionKey));
            var configuration = services.GetService<IConfiguration>();
            var value = configuration?.GetSection(sectionKey)?.Value;
            return services.GetFileText(value, fileNotExistsMsg);

        }

        /// <summary>
        /// 读取全部文本
        /// </summary>
        /// <param name="fileInfo">文件信息接口</param>
        /// <returns></returns>
        private static string ReadAllText(IFileInfo fileInfo)
        {
            byte[] buffer;
            using var stream = fileInfo.CreateReadStream();
            buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return Encoding.Default.GetString(buffer).Trim();
        }

        /// <summary>
        /// 添加文件提供器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileProvider(this IServiceCollection services)
        {

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            return services.AddSingleton<IFileProvider>(new PhysicalFileProvider(basePath));

        }

    }
}
