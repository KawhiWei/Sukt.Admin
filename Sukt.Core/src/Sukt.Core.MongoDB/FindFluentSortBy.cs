using MongoDB.Driver;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Sukt.Core.Shared
{
    public static class FindFluentSortBy<TEntity, TProjection>
    {
        private static readonly ConcurrentDictionary<string, Expression<Func<TEntity, object>>> Cache = new ConcurrentDictionary<string, Expression<Func<TEntity, object>>>();
        public static IOrderedFindFluent<TEntity, TProjection> OrderBy(IFindFluent<TEntity, TProjection> findFluent, string propertyName, Enums.SortDirectionEnum sortDirection)
        {

            propertyName.NotNullOrEmpty("propertyName");
            var keySelector = GetKeySelector(propertyName);
            return sortDirection == Enums.SortDirectionEnum.Ascending
                ? IFindFluentExtensions.SortBy<TEntity, TProjection>(findFluent, keySelector) : IFindFluentExtensions.SortByDescending<TEntity, TProjection>(findFluent, keySelector);
        }

        public static IOrderedFindFluent<TEntity, TProjection> ThenBy(IOrderedFindFluent<TEntity, TProjection> orderedFind, string propertyName, Enums.SortDirectionEnum sortDirection)
        {
            propertyName.NotNullOrEmpty("propertyName");
            var keySelector = GetKeySelector(propertyName);
            return sortDirection == Enums.SortDirectionEnum.Ascending
                ? IFindFluentExtensions.ThenBy<TEntity, TProjection>(orderedFind, keySelector)
                : IFindFluentExtensions.ThenByDescending<TEntity, TProjection>(orderedFind, keySelector);
        }

        private static Expression<Func<TEntity, object>> GetKeySelector(string keyName)
        {
            Type type = typeof(TEntity);
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
                    throw new SuktAppException($"查找类似 指定对象中不存在名称为“{propertyName}”的属性");
                }
                type = property.PropertyType;
                propertyAccess = Expression.Property(propertyAccess, propertyName);
            }
            var convertExpression = Expression.Convert(propertyAccess, typeof(object));
            Expression<Func<TEntity, object>> keySelector =
            Expression.Lambda<Func<TEntity, object>>(
                convertExpression,
                new ParameterExpression[] {
              param
            });
            Cache[key] = keySelector;
            return keySelector;
        }
    }
}
