using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sukt.Core.Shared.Extensions
{
    /// <summary>
    /// 表达式目录树扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Parser语法分析器
        /// </summary>
        /// <param name="parameter">参数表达式</param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static Expression Parser(ParameterExpression parameter, Expression expression)
        {
            if (expression == null) return null;
            switch (expression.NodeType)
            {
                //一元运算符
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    {
                        var unary = expression as UnaryExpression;
                        var exp = Parser(parameter, unary.Operand);
                        return Expression.MakeUnary(expression.NodeType, exp, unary.Type, unary.Method);
                    }
                //二元运算符
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    {
                        var binary = expression as BinaryExpression;
                        var left = Parser(parameter, binary.Left);
                        var right = Parser(parameter, binary.Right);
                        var conversion = Parser(parameter, binary.Conversion);
                        if (binary.NodeType == ExpressionType.Coalesce && binary.Conversion != null)
                        {
                            return Expression.Coalesce(left, right, conversion as LambdaExpression);
                        }
                        else
                        {
                            return Expression.MakeBinary(expression.NodeType, left, right, binary.IsLiftedToNull,
                                binary.Method);
                        }
                    }
                //其他
                case ExpressionType.Call:
                    {
                        var call = expression as MethodCallExpression;
                        List<Expression> arguments = new List<Expression>();
                        foreach (var argument in call.Arguments)
                        {
                            arguments.Add(Parser(parameter, argument));
                        }
                        var instance = Parser(parameter, call.Object);
                        call = Expression.Call(instance, call.Method, arguments);
                        return call;
                    }
                case ExpressionType.Lambda:
                    {
                        var lambda = expression as LambdaExpression;
                        return Parser(parameter, lambda.Body);
                    }
                case ExpressionType.MemberAccess:
                    {
                        var memberAccess = expression as MemberExpression;
                        if (memberAccess.Expression == null)
                        {
                            memberAccess = Expression.MakeMemberAccess(null, memberAccess.Member);
                        }
                        else
                        {
                            var exp = Parser(parameter, memberAccess.Expression);
                            var member = exp.Type.GetMember(memberAccess.Member.Name).FirstOrDefault();
                            memberAccess = Expression.MakeMemberAccess(exp, member);
                        }
                        return memberAccess;
                    }
                case ExpressionType.Parameter:
                    return parameter;

                case ExpressionType.Constant:
                    return expression;

                case ExpressionType.TypeIs:
                    {
                        var typeis = expression as TypeBinaryExpression;
                        var exp = Parser(parameter, typeis.Expression);
                        return Expression.TypeIs(exp, typeis.TypeOperand);
                    }
                default:
                    throw new Exception($"Unhandled expression type:{expression.NodeType}");
            }
        }

        /// <summary>
        /// 表达树类型转换
        /// </summary>
        /// <typeparam name="TInput">转入类型</typeparam>
        /// <typeparam name="TToProperty">要转成的类型</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<TToProperty, bool>> Cast<TInput, TToProperty>(
            this Expression<Func<TInput, bool>> expression) where TInput : class, new()
            where TToProperty : class, new()

        {
            var p = Expression.Parameter(typeof(TToProperty), "p");
            var x = Parser(p, expression);
            return Expression.Lambda<Func<TToProperty, bool>>(x, p);
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

        ///// <summary>
        ///// And操作
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="expr1"></param>
        ///// <param name="expr2"></param>
        ///// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                return expr2;
            }

            var exp = ReplaceParameter(expr1, expr2, out ParameterExpression newParameter);
            var body = Expression.And(exp.left, exp.right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        /// <summary>
        /// And操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <param name="isAnd"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2, bool isAnd)
        {
            if (expr1 == null)
            {
                return expr2;
            }
            if (!isAnd)
            {
                return expr1;
            }

            return expr1.And(expr2);
        }

        /// <summary>
        /// Or操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                return expr2;
            }

            var exp = ReplaceParameter(expr1, expr2, out ParameterExpression newParameter);
            var body = Expression.Or(exp.left, exp.right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        /// <summary>
        /// Or操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <param name="isAnd"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2, bool isAnd)
        {
            if (expr1 == null)
            {
                return expr2;
            }
            if (!isAnd)
            {
                return expr1;
            }

            return expr1.Or(expr2);
        }

        /// <summary>
        /// 替换参数
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <param name="newParameter"></param>
        /// <returns></returns>
        public static (Expression left, Expression right) ReplaceParameter<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2, out ParameterExpression newParameter)
        {
            newParameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(newParameter);

            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);
            return (left, right);
        }

        public static Dictionary<string, object> ExpressionToDictValues<T>(this Expression<Func<T, T>> expression)
        {
            var dictValues = new Dictionary<string, object>();
            var expressionBody = expression.Body;

            while (expressionBody.NodeType == ExpressionType.Convert || expressionBody.NodeType == ExpressionType.ConvertChecked)
            {
                expressionBody = ((UnaryExpression)expressionBody).Operand;
            }
            var entityType = typeof(T);

            var memberInitExpression = expressionBody as MemberInitExpression;
            if (memberInitExpression == null)
            {
                throw new Exception("无效的转换。表达式必须是MemberInitExpression类型。");
            }

            foreach (var binding in memberInitExpression.Bindings)
            {
                var propertyName = binding.Member.Name;

                var memberAssignment = binding as MemberAssignment;
                if (memberAssignment == null)
                {
                    throw new Exception("无效的转换。表达式MemberBinding必须是MemberAssignment类型。");
                }

                var memberExpression = memberAssignment.Expression;

                // CHECK if the assignement has a property from the entity.
                var hasEntityProperty = false;
                memberExpression.Visit((ParameterExpression p) =>
                {
                    if (p.Type == entityType)
                    {
                        hasEntityProperty = true;
                    }

                    return p;
                });

                if (!hasEntityProperty)
                {
                    object value;

                    var constantExpression = memberExpression as ConstantExpression;

                    if (constantExpression != null)
                    {
                        value = constantExpression.Value;
                    }
                    else
                    {
                        //
                        var lambda = Expression.Lambda(memberExpression, null);
                        value = lambda.Compile().DynamicInvoke();
                    }

                    dictValues.Add(propertyName, value);
                }
                else
                {
                    memberExpression = memberExpression.Visit((MemberExpression m) =>
                    {
                        if (m.Expression.NodeType == ExpressionType.Constant)
                        {
                            var lambda = Expression.Lambda(m, null);
                            var value = lambda.Compile().DynamicInvoke();
                            var c = Expression.Constant(value, m.Type);
                            return c;
                        }

                        return m;
                    });

                    dictValues.Add(propertyName, memberExpression);
                }
            }

            return dictValues;
        }
    }

    public class NewExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression NewParameter { get; private set; }

        public NewExpressionVisitor(ParameterExpression param)
        {
            this.NewParameter = param;
        }

        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this.NewParameter;
        }
    }
}