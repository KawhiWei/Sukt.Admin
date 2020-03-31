using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sukt.Core.Shared.SuktAppModules
{
    /// <summary>
    /// SuktAppModule管理实现
    /// </summary>
    public class SuktAppModuleManager : ISuktAppModuleManager
    {
        public List<SuktAppModuleBase> SuktSourceModules { get; private set; }
        public SuktAppModuleManager()
        {
            SuktSourceModules = new List<SuktAppModuleBase>();
        }
        public IServiceCollection LoadModules(IServiceCollection services)
        {
            var typeFinder = services.GetOrAddSingletonService<ITypeFinder, TypeFinder>();
            var baseType = typeof(SuktAppModuleBase);//反射基类模块
            var moduleTypes = typeFinder.Find(x => x.IsSubclassOf(baseType) && !x.IsAbstract).Distinct().ToArray();///拿到SuktAppModuleBase的所有派生类并且不是抽象类
            if(moduleTypes?.Count()<=0)
            {
                throw new SuktAppException("需要加载的模块未找到！！！");
            }
            SuktSourceModules.Clear();
            var moduleBases = moduleTypes.Select(m => (SuktAppModuleBase)Activator.CreateInstance(m));
            SuktSourceModules.AddRange(moduleBases);
            List<SuktAppModuleBase> modules = SuktSourceModules.ToList();
            foreach (var module in modules)
            {
                services = module.ConfigureServices(services);
            }
            return services;


        }
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            foreach (var module in SuktSourceModules)
            {
                module.Configure(applicationBuilder);
            }
        }

        
    }
}
