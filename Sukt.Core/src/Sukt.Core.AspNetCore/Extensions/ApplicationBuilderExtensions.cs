using Microsoft.AspNetCore.Builder;
using Sukt.Core.Shared.SuktAppModules;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Sukt.Core.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseAppModule<TModuleManager>(this IApplicationBuilder app)
           where TModuleManager : ISuktAppModuleManager
        {

            var moduleManager = app.ApplicationServices.GetService<ISuktAppModuleManager>();
            var modules = moduleManager.SuktSourceModules;
            foreach (var module in modules)
            {

                module.Configure(app);
            }
        }
    }
}
