using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.AOP
{
    /// <summary>
    /// 
    /// </summary>
    public class AopManager : IAopManager
    {
        public void AutoLoadAops(IServiceCollection services)
        {
            var typefinder = services.GetOrAddSingletonService<ITypeFinder, TypeFinder>();
            typefinder.NotNull(nameof(typefinder));
            var typs = typefinder.Find(o => o.IsClass && !o.IsAbstract && !o.IsInterface && o.IsSubclassOf(typeof(AbstractInterceptorAttribute)));
            if(typs?.Length>0)
            {
                foreach (var item in typs)
                {
                    services.AddTransient(item);
                }
            }
        }
    }
}
