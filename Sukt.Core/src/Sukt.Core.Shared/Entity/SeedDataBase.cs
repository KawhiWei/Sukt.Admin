using System;
using System.Linq.Expressions;

namespace Sukt.Core.Shared.Entity
{
    public abstract class SeedDataBase<TEntity, TKey> : ISeedData where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public IServiceProvider _serviceProvider = null;

        public SeedDataBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual int Order { get; protected set; } = 0;

        public virtual bool Disable { get; protected set; } = false;

        public virtual void Initialize()
        {
            var entities = SetSeedData();
            SaveDatabase(entities);
        }

        protected abstract TEntity[] SetSeedData();

        /// <summary>
        /// 异步保存到数据库中
        /// </summary>
        /// <returns></returns>
        protected abstract void SaveDatabase(TEntity[] entities);

        protected abstract Expression<Func<TEntity, bool>> Expression(TEntity entity);
    }
    public abstract class SeedDataAggregateBase<TEntity, TKey> : ISeedData where TEntity : IAggregateRoot<TKey> where TKey : IEquatable<TKey>
    {
        public IServiceProvider _serviceProvider = null;

        public SeedDataAggregateBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual int Order { get; protected set; } = 0;

        public virtual bool Disable { get; protected set; } = false;

        public virtual void Initialize()
        {
            var entities = SetSeedData();
            SaveDatabase(entities);
        }

        protected abstract TEntity[] SetSeedData();

        /// <summary>
        /// 异步保存到数据库中
        /// </summary>
        /// <returns></returns>
        protected abstract void SaveDatabase(TEntity[] entities);

        protected abstract Expression<Func<TEntity, bool>> Expression(TEntity entity);
    }
}
