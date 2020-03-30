using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.SuktDependencyAppModule
{
    /// <summary>
    /// IOC管理
    /// </summary>
    public class SuktIocManage
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        private IServiceProvider _provider;
        /// <summary>
        /// 服务集合
        /// </summary>
        private IServiceCollection _services;
        /// <summary>
        /// 创建懒加载Ioc管理实例
        /// </summary>
        private static readonly Lazy<SuktIocManage> SuktInstanceLazy = new Lazy<SuktIocManage>(() => new SuktIocManage());
        /// <summary>
        /// 构造方法
        /// </summary>
        private SuktIocManage()
        {

        }
        public static SuktIocManage Instance => SuktInstanceLazy.Value;
        /// <summary>
        /// 设置应用程序服务提供者
        /// </summary>
        internal void SetApplicationServiceProvider(IServiceProvider provider)
        {
            _provider.NotNull(nameof(provider));
            _provider = provider;
        }
        /// <summary>
        /// 设置应用程序服务集合
        /// </summary>
        internal void SetServiceCollection(IServiceCollection services)
        {
            services.NotNull(nameof(services));
            _services = services;
        }

    }
}
