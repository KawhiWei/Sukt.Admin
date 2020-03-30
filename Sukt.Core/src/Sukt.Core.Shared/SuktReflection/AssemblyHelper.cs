using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Sukt.Core.Shared.SuktReflection
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <returns></returns>
        private static IList<Assembly> GetAllAssemblies()
        {

            string[] filters =
            {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };
            List<Assembly> list = new List<Assembly>();
            var deps = DependencyContext.Default;
            //排除所有的系统程序集、Nuget下载包
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" && !filters.Any(lib.Name.StartsWith));

            try
            {
                foreach (var lib in libs)
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return list;
        }

        public static Assembly[] FindAllItems()
        {
            return GetAllAssemblies().ToArray();
        }
    }
}
