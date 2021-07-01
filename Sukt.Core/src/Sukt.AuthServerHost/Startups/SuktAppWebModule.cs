using Sukt.AutoMapper;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Host.Startups
{
    [SuktDependsOn(
        typeof(SuktAuthBaseModule),
        typeof(SuktAutoMapperModuleBase),
        typeof(DependencyAppModule)
        )]
    public class SuktAppWebModule:SuktAppModule
    {

    }
}
