using MongoDB.Driver;
using Sukt.Core.MongoDB.Infrastructure;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Exceptions;
using Sukt.Core.Shared.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sukt.Core.MongoDB.DbContexts
{
    public abstract class MongoDbContextBase : IDisposable
    {
        private readonly MongoDbContextOptions _options;

        protected MongoDbContextBase([NotNull] MongoDbContextOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        private string ConnectionString => _options.ConnectionString;

        /// <summary>
        /// 文档库名称
        /// </summary>
        public IMongoDatabase Database => GetDbContext();

        /// <summary>
        /// 获取一个文档
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TEntity> Collection<TEntity>()
        {
            return Database.GetCollection<TEntity>(GetTableName<TEntity>());
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        private string GetTableName<TEntity>()
        {
            Type t = typeof(TEntity);
            var table = t.GetAttribute<MongoDBTableAttribute>();

            if (table == null)
            {
                return t.Name;
            }
            if (table.TableName.IsNullOrEmpty())
            {
                throw new SuktAppException("Table name does not exist!");
            }
            return table.TableName;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <returns></returns>
        private IMongoDatabase GetDbContext()
        {
            var mongoUrl = new MongoUrl(ConnectionString);
            var databaseName = mongoUrl.DatabaseName;
            if (databaseName.IsNullOrEmpty())
            {
                throw new SuktAppException($"{mongoUrl}不存DatabaseName名!!!");
            }
            var database = new MongoClient(mongoUrl).GetDatabase(databaseName);
            return database;
        }

        public void Dispose()
        {
        }
    }
}