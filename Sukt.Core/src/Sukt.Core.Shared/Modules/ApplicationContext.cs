using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Modules
{
    /// <summary>
    /// 自定义应用上下文
    /// </summary>
    public class ApplicationContext : IServiceProviderAccessor
    {
        public ApplicationContext(IServiceProvider serviceProvider)
        {
            serviceProvider.NotNull(nameof(serviceProvider));
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; set; }
        
    }
}
