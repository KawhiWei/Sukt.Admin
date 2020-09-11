using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Exceptions;

namespace Sukt.Core.MultiTenancy
{
    public class DefaultSuktConnectionStringResolver : ISuktConnectionStringResolver
    {
        private readonly IServiceProvider _serviceProvider = null;
        private readonly TenantInfo _tenantInfo;

        public DefaultSuktConnectionStringResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            this._tenantInfo = _serviceProvider.GetService<TenantInfo>();
        }
        public string Resolve()
        {
            Console.WriteLine(_tenantInfo.Name);
            string connectionString = "server=10.1.40.207;userid=test;pwd=pwd123456;database=Sukt.Core;connectiontimeout=3000;port=3307;Pooling=true;Max Pool Size=300; Min Pool Size=5;";
            return connectionString ;
        }
    }
}
