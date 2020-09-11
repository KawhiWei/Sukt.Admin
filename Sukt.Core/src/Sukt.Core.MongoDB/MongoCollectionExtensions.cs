using MongoDB.Driver;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Extensions.ResultExtensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// MongoDB扩展
    /// </summary>
    public static class MongoCollectionExtensions
    {
        public static async Task<PageResult<TEntity>> ToPageAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> predicate, IPagedRequest request)
        {
            var count = !predicate.IsNotNull() ? await collection.CountDocumentsAsync(predicate) : await collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty);
            var findFluent = collection.Find(predicate).Skip(request.PageRow * (request.PageIndex - 1)).Limit(request.PageRow);

            findFluent = findFluent.OrderBy(request.OrderConditions);
            var lists = await findFluent.ToListAsync();
            return new PageResult<TEntity>() { Data = lists, Message = "加载成功", Success = true, Total = count.AsTo<int>() };

        }

        public static async Task<PageResult<TResult>> ToPageAsync<TEntity, TResult>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> predicate, IPagedRequest request, Expression<Func<TEntity, TResult>> selector)
        {
            var count = !predicate.IsNotNull() ? await collection.CountDocumentsAsync(predicate) : await collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty);
            var findFluent = collection.Find(predicate).Skip(request.PageRow * (request.PageIndex - 1)).Limit(request.PageRow);

            findFluent = findFluent.OrderBy(request.OrderConditions);
            var lists = await findFluent.Project(selector).ToListAsync();
            return new PageResult<TResult>() { Data = lists, Message = "加载成功", Success = true, Total = count.AsTo<int>() };

        }
    }
}
