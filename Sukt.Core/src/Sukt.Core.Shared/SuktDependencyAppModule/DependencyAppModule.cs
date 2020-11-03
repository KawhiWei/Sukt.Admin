using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Attributes.Dependency;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Linq;
using System.Reflection;

namespace Sukt.Core.Shared.SuktDependencyAppModule
{
    /// <summary>
    /// 自动注入模块，继承与SuktAppModuleBase类进行实现
    /// </summary>
    public class DependencyAppModule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var services = context.Services;
            AddAutoInjection(services);
        }

        private void AddAutoInjection(IServiceCollection services)
        {
            var typeFinder = services.GetOrAddSingletonService<ITypeFinder, TypeFinder>();
            var baseTypes = new Type[] { typeof(IScopedDependency), typeof(ITransientDependency), typeof(ISingletonDependency) };
            var types = typeFinder.FindAll().Distinct();
            types = types.Where(type => type.IsClass && !type.IsAbstract && (baseTypes.Any(b => b.IsAssignableFrom(type))) || type.GetCustomAttribute<DependencyAttribute>() != null);
            foreach (var implementedInterType in types)
            {
                var attr = implementedInterType.GetCustomAttribute<DependencyAttribute>();
                var typeInfo = implementedInterType.GetTypeInfo();
                var serviceTypes = typeInfo.ImplementedInterfaces.Where(x => x.HasMatchingGenericArity(typeInfo) && !x.HasAttribute<IgnoreDependencyAttribute>() && x != typeof(IDisposable)).Select(t => t.GetRegistrationType(typeInfo));
                var lifetime = GetServiceLifetime(implementedInterType);
                if (lifetime == null)
                {
                    break;
                }
                if (serviceTypes.Count() == 0)
                {
                    services.Add(new ServiceDescriptor(implementedInterType, implementedInterType, lifetime.Value));
                    continue;
                }
                if (attr?.AddSelf == true)
                {
                    services.Add(new ServiceDescriptor(implementedInterType, implementedInterType, lifetime.Value));
                }
                foreach (var serviceType in serviceTypes.Where(o => !o.HasAttribute<IgnoreDependencyAttribute>()))
                {
                    services.Add(new ServiceDescriptor(serviceType, implementedInterType, lifetime.Value));
                }
            }
        }

        private ServiceLifetime? GetServiceLifetime(Type type)
        {
            var attr = type.GetCustomAttribute<DependencyAttribute>();
            if (attr != null)
            {
                return attr.Lifetime;
            }

            if (typeof(IScopedDependency).IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            if (typeof(ITransientDependency).IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            return null;
        }

        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
        }

        //public override IServiceCollection ConfigureServices(IServiceCollection service)
        //{
        //    SuktIocManage.Instance.SetServiceCollection(service);//写入服务集合
        //    this.BulkIntoServices(service);
        //    return service;
        //}
        ///// <summary>
        ///// 批量注入服务
        ///// </summary>
        ///// <param name="services"></param>
        //private void BulkIntoServices(IServiceCollection services)
        //{
        //    var typeFinder = services.GetOrAddSingletonService<ITypeFinder, TypeFinder>();
        //    typeFinder.NotNull(nameof(typeFinder));
        //    Type[] dependencyTypes = typeFinder.Find(type => type.IsClass && !type.IsAbstract && !type.IsInterface && type.HasAttribute<DependencyAttribute>());
        //    foreach (var dependencyType in dependencyTypes)
        //    {
        //        AddToServices(services, dependencyType);
        //    }
        //}
        ///// <summary>
        ///// 将服务实现类型注册到服务集合中
        ///// </summary>
        ///// <param name="services">服务集合</param>
        ///// <param name="implementationType">要注册的服务实例类型</param>
        //protected virtual void AddToServices(IServiceCollection services, Type implementationType)
        //{
        //    var atrr = implementationType.GetAttribute<DependencyAttribute>();
        //    Type[] serviceTypes = implementationType.GetImplementedInterfaces().Where(o => !o.HasAttribute<IgnoreDependencyAttribute>()).ToArray();
        //    if (serviceTypes.Length == 0)
        //    {
        //        services.TryAdd(new ServiceDescriptor(implementationType, implementationType, atrr.Lifetime));
        //        return;
        //    }
        //    if (atrr?.AddSelf == true)
        //    {
        //        services.TryAdd(new ServiceDescriptor(implementationType, implementationType, atrr.Lifetime));
        //    }
        //    foreach (var interfaceType in serviceTypes)
        //    {
        //        services.Add(new ServiceDescriptor(interfaceType, implementationType, atrr.Lifetime));
        //    }
        //}
    }
}