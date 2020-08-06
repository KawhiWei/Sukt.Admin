using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.Shared.Events
{
    public class EventBusAppModuleBase:SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            var assemblys = service.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>()?.FindAll();
            service.AddMediatR(assemblys);
            service.AddEvents();
        }
    }
}
