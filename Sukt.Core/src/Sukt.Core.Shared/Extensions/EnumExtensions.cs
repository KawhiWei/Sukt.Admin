using Sukt.Core.Shared.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Sukt.Core.Shared.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转成显示名字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            var type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member.ToDescription();
        }

        /// <summary>
        /// 得到枚举值指定特性下描述
        /// </summary>
        /// <typeparam name="TAttribute">要得到的特性</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string ToDescription<TAttribute>(this Enum value)
                where TAttribute : AttributeBase
        {
            var type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member.ToDescription<TAttribute>();
        }
    }
}