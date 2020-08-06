using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Modules
{
    public class ModulesOptions
    {
        public IServiceCollection Service { get; }

        public ModulesOptions(IServiceCollection service)
        {
            Service = service;
        }
    }
}
