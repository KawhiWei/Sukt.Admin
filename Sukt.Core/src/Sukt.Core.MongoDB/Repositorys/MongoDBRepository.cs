using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.MongoDB.DbContexts;
using MongoDB.Driver.Linq;

namespace Sukt.Core.MongoDB.Repositorys
{
    public class MongoDBRepository<TData, Tkey> : IMongoDBRepository<TData, Tkey>
    {

        private readonly IMongoCollection<TData> _collection;
        private readonly MongoDbContextBase _mongoDbContext;
        public MongoDBRepository(IServiceProvider serviceProvider, MongoDbContextBase mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _collection = _mongoDbContext.Collection<TData>();
        }

        public async Task InsertAsync(TData entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task InsertAsync(List<TData> entitys)
        {
            await _collection.InsertManyAsync(entitys);
        }
        public virtual IMongoQueryable<TData> Entities => _collection.AsQueryable();
    }
}
