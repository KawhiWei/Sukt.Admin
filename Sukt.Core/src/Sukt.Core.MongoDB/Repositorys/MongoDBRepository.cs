using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Sukt.Core.MongoDB.DbContexts;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sukt.Core.MongoDB.Repositorys
{
    public class MongoDBRepository<TData, Tkey> : IMongoDBRepository<TData, Tkey> where TData : class, IEntity<Tkey>
       where Tkey : IEquatable<Tkey>
    {
        private readonly IMongoCollection<TData> _collection;
        private readonly MongoDbContextBase _mongoDbContext;
        public virtual IMongoCollection<TData> Collection { get; private set; }

        public MongoDBRepository(IServiceProvider serviceProvider, MongoDbContextBase mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _collection = _mongoDbContext.Collection<TData>();
            Collection = _mongoDbContext.Collection<TData>();
        }

        public async Task InsertAsync(TData entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task InsertAsync(List<TData> entitys)
        {
            await _collection.InsertManyAsync(entitys);
        }

        public virtual IMongoQueryable<TData> Entities => CreateQuery();

        public async Task<TData> FindByIdAsync(Tkey key)
        {
            return await Collection.Find(CreateEntityFilter(key)).FirstOrDefaultAsync();
        }

        public async Task<OperationResponse> UpdateAsync(Tkey key, UpdateDefinition<TData> update)
        {
            var filters = this.CreateEntityFilter(key);
            var result = await Collection.UpdateManyAsync(filters, update);
            return result.ModifiedCount > 0 ? OperationResponse.Ok("更新成功") : OperationResponse.Error("更新失败");
        }

        public async Task<OperationResponse> DeleteAsync(Tkey key)
        {
            var filters = this.CreateEntityFilter(key);
            var result = await Collection.DeleteOneAsync(filters);
            return result.DeletedCount > 0 ? OperationResponse.Ok("删除成功") : OperationResponse.Error("删除失败");
        }

        private IMongoQueryable<TData> CreateQuery()
        {
            var entities = Collection.AsQueryable();
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TData)))
            {
                entities = entities.Where(m => ((ISoftDelete)m).IsDeleted == false);
            }
            return entities;
        }

        private FilterDefinition<TData> CreateEntityFilter(Tkey id)
        {
            var filters = new List<FilterDefinition<TData>>
            {
                Builders<TData>.Filter.Eq(e => e.Id, id)
            };
            AddGlobalFilters(filters);
            return Builders<TData>.Filter.And(filters);
        }

        private void AddGlobalFilters(List<FilterDefinition<TData>> filters)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TData)))
            {
                filters.Add(Builders<TData>.Filter.Eq(e => ((ISoftDelete)e).IsDeleted, false));
            }
        }

        private Expression<Func<TData, bool>> CreateExpression(Expression<Func<TData, bool>> expression)
        {
            Expression<Func<TData, bool>> expression1 = o => true;
            if (expression == null)
            {
                expression = o => true;
            }
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TData)))
            {
                expression1 = m => ((ISoftDelete)m).IsDeleted == false;
                expression = expression.And(expression1);
            }
            return expression;
        }

    }
}