using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
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
            var configuration = serviceProvider.GetService<IConfiguration>();
            var Dbpath = configuration["SuktCore:DbContext:MongoDBConnectionString"];
            var MongoDBDataBase = configuration["SuktCore:DbContext:MongoDBDataBase"];
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            var dbcontext = Path.Combine(basePath, Dbpath);
            if (!File.Exists(dbcontext))
            {
                throw new Exception("未找到存放数据库链接的文件");
            }
            var connection = File.ReadAllText(dbcontext).Trim();
            var client = new MongoClient(connection);
            var database = client.GetDatabase(MongoDBDataBase);
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
