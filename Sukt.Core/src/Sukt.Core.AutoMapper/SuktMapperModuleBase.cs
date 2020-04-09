using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Attributes.AutoMapper;
using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktAppModules;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Linq;
using System.Reflection;

namespace Sukt.Core.AutoMapper
{
    public abstract class SuktMapperModuleBase: SuktAppModuleBase
    {
        /// <summary>
        /// 重写SuktAppModuleBase
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            var assemblyFinder = service.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>();
            var assemblys = assemblyFinder.FindAll();
            var suktAutoMapTypes = assemblys.SelectMany(x => x.GetTypes()).Where(s => s.IsClass && !s.IsAbstract && s.HasAttribute<SuktAutoMapperAttribute>(true)).Distinct().ToArray();
            service.AddAutoMapper(mapper =>
            {
                this.CreateMapping<SuktAutoMapperAttribute>(suktAutoMapTypes, mapper);
            },assemblys,ServiceLifetime.Singleton);
            var mapper = service.GetService<IMapper>();//获取autoMapper实例
            AutoMapperExtension.SetMapper(mapper);
            return base.ConfigureServices(service);
        }
        /// <summary>
        /// 创建扩展方法
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="sourceTypes"></param>
        /// <param name="mapperConfigurationExpression"></param>
        private void CreateMapping<TAttribute>(Type[] sourceTypes, IMapperConfigurationExpression mapperConfigurationExpression)where TAttribute : SuktAutoMapperAttribute
        {
            foreach (var sourceType in sourceTypes)
            {
                var attribute = sourceType.GetCustomAttribute<TAttribute>();
                if (attribute.TargetTypes?.Count() <= 0)
                {
                    return;
                }
                foreach (var tatgetType in attribute.TargetTypes)
                {
                    ///判断是To
                    if (attribute.MapDirection.HasFlag(SuktAutoMapDirection.To))
                    {
                        mapperConfigurationExpression.CreateMap(sourceType, tatgetType);
                    }
                    ///判断是false
                    if (attribute.MapDirection.HasFlag(SuktAutoMapDirection.From))
                    {
                        mapperConfigurationExpression.CreateMap(tatgetType, sourceType);
                    }

                }
            }
        }
    }
}
