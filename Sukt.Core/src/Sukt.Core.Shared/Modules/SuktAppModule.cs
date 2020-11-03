using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sukt.Core.Shared.Modules
{
    public class SuktAppModule : ISuktAppModule
    {
        public virtual void ApplicationInitialization(ApplicationContext context)
        {
        }

        public virtual void ConfigureServices(ConfigureServicesContext context)
        {
        }

        /// <summary>
        /// 获取模块程序集
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public Type[] GetDependedTypes(Type moduleType = null)
        {
            if (moduleType == null)
            {
                moduleType = GetType();
            }
            var dependedTypes = moduleType.GetCustomAttributes().OfType<IDependedTypesProvider>().ToArray();
            if (dependedTypes.Length == 0)
            {
                return new Type[0];
            }
            List<Type> dependList = new List<Type>();
            foreach (var dependedType in dependedTypes)
            {
                var dependeds = dependedType.GetDependedTypes();
                if (dependeds.Length == 0)
                {
                    continue;
                }
                dependList.AddRange(dependeds);

                foreach (Type type in dependeds)
                {
                    dependList.AddRange(GetDependedTypes(type));
                }
            }
            return dependList.Distinct().ToArray();
        }

        /// <summary>
        /// 判断是否是模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAppModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass &&
                 !typeInfo.IsAbstract &&
                 !typeInfo.IsGenericType &&
                 typeof(ISuktAppModule).GetTypeInfo().IsAssignableFrom(type);
        }
    }
}