using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktAppModules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Sukt.Core.Shared.Extensions;

namespace Sukt.Core.Shared.Events
{
    public abstract class EventBusAppModuleBase:SuktAppModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            var assemblys = service.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>()?.FindAll();
            service.AddMediatR(assemblys);
            service.AddEvents();
            return service;
        }
    }
}
