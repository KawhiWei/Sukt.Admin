using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Attributes.AutoMapper
{
    /// <summary>
    /// 如果使用AutoMapper会跟官方冲突，所以在前面加了项目代号
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SuktAutoMapperAttribute:Attribute
    {
        /// <summary>
        /// 构造函数传值
        /// </summary>
        /// <param name="targetTypes"></param>
        public SuktAutoMapperAttribute(params Type[] targetTypes)
        {
            targetTypes.NotNull(nameof(targetTypes));
            TargetTypes = targetTypes;
        }
        /// <summary>
        /// 类型数组
        /// </summary>
        public Type[] TargetTypes { get; private set; }
        public virtual SuktAutoMapDirection MapDirection
        {
            get { return SuktAutoMapDirection.From | SuktAutoMapDirection.To; }
        }

    }
}
