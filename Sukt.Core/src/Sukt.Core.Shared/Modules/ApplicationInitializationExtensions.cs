using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Modules
{
    public static class ApplicationInitializationExtensions
    {
        public static IApplicationBuilder GetApplicationBuilder(this ApplicationContext applicationContext)
        {
            return applicationContext.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
        }
    }
}
