using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.AOP;
using Sukt.Core.Shared.SuktAppModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.API.Startups
{
    public class AspNetCoreAOPModule : SuktAppModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {

            //service.AddSingleton<IAopManager>(pro =>
            //{
            //    var aopManager = new AopManager();
            //    aopManager.AutoLoadAops(service);
            //    return aopManager;
            //});
            return service;
        }
    }
}
