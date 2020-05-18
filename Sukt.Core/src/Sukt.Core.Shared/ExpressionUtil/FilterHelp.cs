using Microsoft.EntityFrameworkCore;
using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Filter;
using Sukt.Core.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;

namespace Sukt.Core.Shared.ExpressionUtil
{
    public static class FilterHelp
    {
        public static Expression<Func<T, bool>> GetExpression<T>(QueryFilter filterItem)
        {

            ParameterExpression param = Expression.Parameter(typeof(T), "m");


            Expression expression = GetExpressionBody(param, filterItem);

            return Expression.Lambda<Func<T, bool>>(expression, param);
        }
        private static Expression GetExpressionBody(ParameterExpression param, QueryFilter filterItem)
        {
            List<Expression> expressions = new List<Expression>();
            Expression expression = Expression.Constant(true);
            //expression = GetExpressionBody(param, filterItem.Filters, filterItem.Logic);
            foreach (var item in filterItem.Filters)
            {
                expressions.Add(GetExpressionBody(param, item));
            }
            //foreach (var item in filterItem.FilterItems)
            //{
            //    expressions.Add(GetExpressionBody(param, item));
            //}

            if (filterItem.FilterConnect == FilterConnect.And)
            {
                return expressions.Aggregate(Expression.AndAlso);
            }
            else
            {
                return expressions.Aggregate(Expression.OrElse);
            }
        }
        private static Expression GetExpressionBody(ParameterExpression param, ICollection<FilterCondition> filters, FilterConnect logic)
        {
            Expression expression = Expression.Constant(true);
            foreach (var filter in filters)
            {
                if (logic == FilterConnect.And)
                {
                    expression = Expression.AndAlso(expression, GetExpressionBody(param, filter));
                }
                else
                {
                    expression = Expression.AndAlso(expression, GetExpressionBody(param, filter));
                }
            }
            return expression;
        }
        private static Expression GetExpressionBody(ParameterExpression param, FilterCondition filter)
        {
            try
            {
                var lambda = GetPropertyLambdaExpression(param, filter);
                ConstantExpression constant = Expression.Constant(true);
                //object value = null;
                if (lambda.Body.Type == typeof(Guid))
                {
                    Guid.TryParse(filter.Value.ToString(), out var value);
                    constant = Expression.Constant(value, lambda.Body.Type);
                }
                else
                {
                    constant = Expression.Constant(filter.Value, lambda.Body.Type);
                }

                return GetOperateExpression(filter.Operator, lambda.Body, constant);
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        private static Expression GetOperateExpression(FilterOperator operate, Expression member, Expression expression2)
        {
            switch (operate)
            {
                case FilterOperator.Equal:
                    return Expression.Equal(member, expression2);

                case FilterOperator.NotEqual:
                    return Expression.NotEqual(member, expression2);

                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(member, expression2);

                case FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, expression2);

                case FilterOperator.LessThan:
                    return Expression.LessThan(member, expression2);

                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, expression2);

                case FilterOperator.Like:

                    return Like(member, expression2);
                default:
                    return null;

            }
        }


        private static Expression Like(Expression member, Expression expression2)
        {
            if (expression2.Type != typeof(string))
            {
                throw new NotSupportedException("“Like”比较方式只支持字符串类型的数据");
            }
            var functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions)));
            var like = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { functions.Type, typeof(string), typeof(string) });
            var methodCallExpression = Expression.Call(
             null,
             like,
             functions,
             member,
             expression2);
            return methodCallExpression;
        }
        private static LambdaExpression GetPropertyLambdaExpression(ParameterExpression parameter, FilterCondition filter)
        {
            var type = parameter.Type;
            var property = type.GetProperty(filter.Field);
            if (property == null)
            {
            }
            Expression propertyAccess = Expression.MakeMemberAccess(parameter, property);
            return Expression.Lambda(propertyAccess, parameter);
        }
    }
}
