using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uwl.Common.Sort.SortEnum;
using Uwl.Extends.Infrastructure;
using Uwl.Extends.Sort;

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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> PageBy<TEntity>(this IQueryable<TEntity> source,Parameters parameters)
        {
            try
            {
                IOrderedQueryable<TEntity> orderSource = null;
                if (parameters.OrderConditions == null || parameters.OrderConditions.Length == 0)
                {
                    orderSource = CollectionPropertySorter<TEntity>.OrderBy(source, "Id", SortDirectionEnum.Ascending);
                }
                int count = 0;
                foreach (OrderCondition orderCondition in parameters.OrderConditions)
                {
                    orderSource = count == 0
                        ? CollectionPropertySorter<TEntity>.OrderBy(source, orderCondition.SortField, orderCondition.SortDirection)
                        : CollectionPropertySorter<TEntity>.ThenBy(orderSource, orderCondition.SortField, orderCondition.SortDirection);
                    count++;
                }
                source = orderSource;
                if(!source.IsNull())
                {
                    return source.Skip(parameters.PageSize*(parameters.PageIndex-1)).Take(parameters.PageSize);
                }
                else
                {
                   return Enumerable.Empty<TEntity>().AsQueryable();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        /// <summary>
        /// 模型验证
        /// </summary>
        /// <typeparam name="T">带有必填标签的类</typeparam>
        /// <param name="t">出入model</param>
        /// <param name="top">获取前多个问题</param>
        /// <returns></returns>
        public static Tuple<bool, string> CheckModel<T>(this T t, int top = 1) where T : new()
        {
            if (t == null) return new Tuple<bool, string>(false, "无法验证");
            var val = new ValidationContext(t);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(t, val, results, true);
            if (!isValid)
                return new Tuple<bool, string>(false, string.Join(",", results.Select(x => x.ErrorMessage).Take(top).ToList()));
            return new Tuple<bool, string>(true, "验证通过");
        }

        /// <summary>
        /// 得到表达树对应属性的名字
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyName<TEntity>(this Expression<Func<TEntity, object>> expr)
        {
            expr.NotNull(nameof(expr));
            var name = string.Empty;
            var body = expr.Body;
            if (body is UnaryExpression)
            {
                var unaryExpression = body as UnaryExpression;
                var operand = unaryExpression.Operand;
                var memberExpression = operand as MemberExpression;
                name = memberExpression?.Member.Name;

            }
            else if (body is MemberExpression)
            {
                var memberExpression = body as MemberExpression;

                name = memberExpression.Member.Name;
            }
            else if (body is ParameterExpression)
            {
                var parameterExpression = body;

                name = parameterExpression.Type.Name;
            }
            return name;
        }
        /// <summary>
        /// 检查参数不能为空引用，否则抛出<see cref="ArgumentNullException"/>异常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull<T>(this T value, string paramName)
        {
            Require<ArgumentNullException>(value != null, $"参数“{paramName}”不能为空引用。");
        }
        /// <summary>
        /// 验证指定值的断言<paramref name="assertion"/>是否为真，如果不为真，抛出指定消息<paramref name="message"/>的指定类型<typeparamref name="TException"/>异常
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="assertion">要验证的断言。</param>
        /// <param name="message">异常消息。</param>
        private static void Require<TException>(bool assertion, string message)
            where TException : Exception
        {
            if (assertion)
            {
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }

        /// <summary>
        /// 检查字符串不能为空引用或空字符串，否则抛出<see cref="ArgumentNullException"/>异常或<see cref="ArgumentException"/>异常。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">参数名称。</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrEmpty(this string value, string paramName)
        {
            Require<ArgumentException>(!string.IsNullOrEmpty(value), $"参数“{paramName}”不能为空引用或空字符串。");
        }
        /// <summary>
        /// 是否为Null
        /// </summary>
        /// <param name="value">判断的值</param>
        /// <returns>true为null,false不为null</returns>
        public static bool IsNull(this object value)
        {

            return value == null ? true : false;
        }
    }
}
