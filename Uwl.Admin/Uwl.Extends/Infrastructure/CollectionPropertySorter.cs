using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Uwl.Common.Sort.SortEnum;
using Uwl.Extends.Utility;

namespace Uwl.Extends.Infrastructure
{
    public class CollectionPropertySorter<T>
    {
        private static readonly ConcurrentDictionary<string, LambdaExpression> Cache = new ConcurrentDictionary<string, LambdaExpression>();

        /// <summary>
        /// 按指定的属性名称对<see cref="IQueryable{T}"/>序列进行排序
        /// </summary>
        /// <param name="source">IQueryable{T}序列</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string propertyName, SortDirectionEnum sortDirection)
        {
            propertyName.NotNullOrEmpty("propertyName");
            dynamic keySelector = GetKeySelector(propertyName);
            return sortDirection == SortDirectionEnum.Ascending
                ? Queryable.OrderBy(source, keySelector)
                : Queryable.OrderByDescending(source, keySelector);
        }


        /// <summary>
        /// 按指定的属性名称对<see cref="IOrderedQueryable{T}"/>序列进行排序
        /// </summary>
        /// <param name="source">IOrderedQueryable{T}序列</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string propertyName, SortDirectionEnum sortDirection)
        {
            propertyName.NotNullOrEmpty("propertyName");
            dynamic keySelector = GetKeySelector(propertyName);
            return sortDirection == SortDirectionEnum.Ascending
                ? Queryable.ThenBy(source, keySelector)
                : Queryable.ThenByDescending(source, keySelector);
        }

        private static LambdaExpression GetKeySelector(string keyName)
        {
            Type type = typeof(T);
            string key = $"{type.FullName}.{keyName}";
            if (Cache.ContainsKey(key))
            {
                return Cache[key];
            }
            ParameterExpression param = Expression.Parameter(type);
            string[] propertyNames = keyName.Split(".");
            Expression propertyAccess = param;

            foreach (var propertyName in propertyNames)
            {
                PropertyInfo property = type.GetProperty(propertyName);
                if (property.IsNull())
                {
                    throw new Exception($"查找类似 指定对象中不存在名称为“{propertyName}”的属性");
                }
                type = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
            LambdaExpression keySelector = Expression.Lambda(propertyAccess, param);
            Cache[key] = keySelector;
            return keySelector;
        }
    }
}
