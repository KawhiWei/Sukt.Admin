using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Uwl.Common.LambdaTree
{
    /// <summary>
    /// 添加Lambda表达式拼接多条件查询
    /// </summary>
    internal class LambdaExpressionVisitor: ExpressionVisitor
    {
        /// <summary>
        /// 定义一个参数表达式
        /// </summary>
        public ParameterExpression ParameterExpression { get; private set; }
        /// <summary>
        /// 通过构造函数获取参数
        /// </summary>
        /// <param name="paramExpr"></param>
        public LambdaExpressionVisitor(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }
        /// <summary>
        /// 返回修改后的表达式
        /// </summary>
        /// <returns></returns>
        public Expression Replace(Expression expression)
        {
            return this.Visit(expression);
        }
        /// <summary>
        /// 重写访问参数方法
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            //返回自定义参数
            return this.ParameterExpression; 
        }
        //var useragreecount =
        //    _postgreDbContext.agreement.Join(_postgreDbContext.user_agreement.Where(p => p.userid == userId), 

        //        e => e.id, p => p.agreement_id, (e, p) => e).Where(e => e.type == AgreementType.DZ).Count();
    }
    /// <summary>
    /// 定义一个调用方法
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// 定义表达式主体，返回True或者False
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        /// <summary>
        /// 重写And函数
        /// </summary>
        /// <typeparam name="T">传入的实体类型</typeparam>
        /// <param name="expression_left">表达式主体的左边</param>
        /// <param name="expression_right">表达式主体的右边</param>
        /// <returns>返回拼接的表达式</returns>
        public static Expression<Func<T,bool>> And<T>(this Expression<Func<T,bool>> expression_left, Expression<Func<T, bool>> expression_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new LambdaExpressionVisitor(candidateExpr);
            var left = parameterReplacer.Replace(expression_left.Body);//例如这个我就只知道拿到左边的对象
            var right = parameterReplacer.Replace(expression_right.Body);//拿到右边的对象
            var body = Expression.And(left, right);//在这个里面进行合并
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
        /// <summary>
        /// 重写Or函数
        /// </summary>
        /// <typeparam name="T">传入的实体类型</typeparam>
        /// <param name="expression_left">表达式主体的左边</param>
        /// <param name="expression_right">表达式主体的右边</param>
        /// <returns>返回拼接的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression_left, Expression<Func<T, bool>> expression_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new LambdaExpressionVisitor(candidateExpr);
            var left = parameterReplacer.Replace(expression_left.Body);
            var right = parameterReplacer.Replace(expression_right.Body);
            var body = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
}
