using Sukt.Core.Shared.Enums;
using System;
using System.Linq.Expressions;

namespace Sukt.Core.Shared.Extensions.OrderExtensions
{
    public class OrderCondition
    {
        // <summary>
        /// 初始化一个<see cref="OrderCondition"/>类型的新实例
        /// </summary>
        public OrderCondition() : this(null)
        { }

        /// <summary>
        /// 构造一个指定字段名称的升序排序的排序条件
        /// </summary>
        /// <param name="sortField">字段名称</param>
        public OrderCondition(string sortField)
            : this(sortField, SortDirectionEnum.Ascending)
        { }

        /// <summary>
        /// 构造一个排序字段名称和排序方式的排序条件
        /// </summary>
        /// <param name="sortField">字段名称</param>
        /// <param name="sortDirection">排序方式</param>
        public OrderCondition(string sortField, SortDirectionEnum sortDirection)
        {
            SortField = sortField;
            SortDirection = sortDirection;
        }

        /// <summary>
        /// 获取或设置 排序字段名称
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 获取或设置 排序方向
        /// </summary>
        public SortDirectionEnum SortDirection { get; set; }
    }

    public class OrderCondition<T> : OrderCondition
    {
        /// <summary>
        /// 使用排序字段 初始化一个<see cref="OrderCondition"/>类型的新实例
        /// </summary>
        public OrderCondition(Expression<Func<T, object>> keySelector)
            : this(keySelector, SortDirectionEnum.Ascending)
        { }

        /// <summary>
        /// 使用排序字段与排序方式 初始化一个<see cref="OrderCondition"/>类型的新实例
        /// </summary>
        public OrderCondition(Expression<Func<T, object>> keySelector, SortDirectionEnum sortDirection)
            : base(GetPropertyName(keySelector), sortDirection)
        { }

        /// <summary>
        /// 从泛型委托获取属性名
        /// </summary>
        private static string GetPropertyName(Expression<Func<T, object>> keySelector)
        {
            return keySelector.GetPropertyName();
        }
    }
}