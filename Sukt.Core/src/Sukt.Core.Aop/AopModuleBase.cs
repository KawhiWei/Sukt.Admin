using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Aop.Aop;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktAppModules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;

namespace Sukt.Core.Aop
{
    /// <summary>
    /// 全局AOP模块
    /// </summary>
    public abstract class AopModuleBase: SuktAppModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            var typefinder = service.GetOrAddSingletonService<ITypeFinder, TypeFinder>();
            typefinder.NotNull(nameof(typefinder));
            var typs = typefinder.Find(o => o.IsClass && !o.IsAbstract && !o.IsInterface && o.IsSubclassOf(typeof(AbstractInterceptor)));
            var InterceptorsModule = service.GetConfiguration()["SuktCore:InterceptorsModule"];
            //var IInterceptorsModule = service.GetConfiguration()["SuktCore:IInterceptorsModule"];
            
            if (typs?.Length > 0)
            {
                List<Type> types = new List<Type>();
                //types.Add(typeof(AopTran));
                foreach (var item in typs)
                {
                    //service.AddTransient(item);
                    service.ConfigureDynamicProxy(cof =>
                    {
                        var Enabled = service.GetConfiguration()[$"SuktCore:AopManager:{item.Name}:Enabled"].ObjToBool();
                        if(Enabled)
                            cof.Interceptors.AddTyped(item, Predicates.ForNameSpace(InterceptorsModule)/*,Predicates.ForNameSpace(IInterceptorsModule)*/);////这种是配置只需要代理的层, Predicates.ForNameSpace("Sukt.Core.Application.Contracts")
                        //config.NonAspectPredicates.AddService("IUnitofWork");//需要过滤掉不需要代理的服务层  
                    });
                }
            }
            return service; 
        }
    }
}
