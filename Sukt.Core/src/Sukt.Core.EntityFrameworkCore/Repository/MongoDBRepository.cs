using MongoDB.Driver;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    public class MongoDBRepository<TData, Tkey> : IMongoDBRepository<TData, Tkey>
    {
        private readonly IMongoCollection<TData> _collection;

        public MongoDBRepository(IServiceProvider serviceProvider)
        {
            var client = new MongoClient("mongodb://10.1.40.207:27017");
            var database = client.GetDatabase("IDNAudit");
            Type t = typeof(TData);
            var table = t.GetAttribute<MongoDBTableAttribute>();
            if (table == null)
                throw new SuktAppException("Table name does not exist!");
            _collection = database.GetCollection<TData>(table.TableName);
        }
        public async Task InsertAsync(TData entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task InsertAsync(List<TData> entitys)
        {
            await _collection.InsertManyAsync(entitys);
        }
    }
}
