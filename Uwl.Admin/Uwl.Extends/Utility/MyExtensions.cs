using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uwl.Extends.Utility
{
    public static class MyExtensions
    {
        /// <summary>
        /// 扩展方法判断是否为空或者为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str);
        }
        /// <summary>
        /// 转换为Guid类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            Guid guid;
            if(Guid.TryParse(str,out guid))
            {
                return guid;
            }
            else
            {
                return Guid.Empty;
            }
        }
        /// <summary>
        ///     字段选择器
        /// </summary>
        /// <param name="select">字段选择器</param>
        /// <param name="IDs">条件，等同于：o=> IDs.Contains(o.ID) 的操作</param>
        /// <param name="lst">列表</param>
        public static List<T> ToSelectList<TEntity, T>(this IEnumerable<TEntity> lst, Func<TEntity, T> select)
        {
            //ExcelHelper.ToEntityList<>
            return lst?.Select(select).ToList();
            
        }
        /// <summary>
        /// 判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type"> 要处理的类型 </param>
        /// <returns> 是返回True，不是返回False </returns>
        public static bool IsNullableType(this Type type)
        {

            return ((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
