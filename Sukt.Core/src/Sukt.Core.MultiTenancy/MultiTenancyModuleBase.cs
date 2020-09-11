using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.MultiTenancy
{
    public abstract class MultiTenancyModuleBase: SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScoped<TenantInfo>();
        }
    }
}
