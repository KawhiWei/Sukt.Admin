using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.Shared.Attributes.Dependency;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktAppModules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sukt.Core.Shared.SuktDependencyAppModule
{
    /// <summary>
    /// 自动注入模块，继承与SuktAppModuleBase类进行实现
    /// </summary>
    public class DependencyAppModule:SuktAppModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            SuktIocManage.Instance.SetServiceCollection(service);//写入服务集合
            this.BulkIntoServices(service);
            return service;
        }
        /// <summary>
        /// 批量注入服务
        /// </summary>
        /// <param name="services"></param>
        private void BulkIntoServices(IServiceCollection services)
        {
            var typeFinder = services.GetOrAddSingletonService<ITypeFinder, TypeFinder>();
            typeFinder.NotNull(nameof(typeFinder));
            Type[] dependencyTypes = typeFinder.Find(type => type.IsClass && !type.IsAbstract && !type.IsInterface && type.HasAttribute<DependencyAttribute>());
            foreach (var dependencyType in dependencyTypes)
            {
                AddToServices(services, dependencyType);
            }
        }
        /// <summary>
        /// 将服务实现类型注册到服务集合中
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="implementationType">要注册的服务实例类型</param>
        protected virtual void AddToServices(IServiceCollection services, Type implementationType)
        {
            var atrr = implementationType.GetAttribute<DependencyAttribute>();
            Type[] serviceTypes = implementationType.GetImplementedInterfaces().Where(o => !o.HasAttribute<IgnoreDependencyAttribute>()).ToArray();
            if (serviceTypes.Length == 0)
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, atrr.Lifetime));
                return;
            }
            if (atrr?.AddSelf == true)
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, atrr.Lifetime));
            }
            foreach (var interfaceType in serviceTypes)
            {
                services.Add(new ServiceDescriptor(interfaceType, implementationType, atrr.Lifetime));
            }
        }
    }
}
