using System;

namespace Sukt.Core.Shared.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SuktDependsOnAttribute : Attribute, IDependedTypesProvider
    {
        public SuktDependsOnAttribute(params Type[] dependedTypes)
        {
            DependedTypes = dependedTypes ?? new Type[0];
        }

        /// <summary>
        /// 依赖类型集合
        /// </summary>
        private Type[] DependedTypes { get; }

        /// <summary>
        /// 得到依赖类型集合
        /// </summary>
        /// <returns></returns>
        public Type[] GetDependedTypes()
        {
            return DependedTypes;
        }
    }
}