using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Shared.Entity;
using System;

namespace Sukt.Core.Shared
{
    public abstract class EntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration<TEntity, TKey> where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Type DbContextType => typeof(DefaultDbContext);

        public Type EntityType => typeof(TEntity);

        public abstract void Map(EntityTypeBuilder<TEntity> b);

        public void Map(ModelBuilder b)
        {
            Map(b.Entity<TEntity>());
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))//判断实体中是否继承软删除接口
            {
                b.Entity<TEntity>().HasQueryFilter(x => ((ISoftDelete)x).IsDeleted == false);
            }
        }
    }
    public abstract class AggregateRootMappingConfiguration<TEntity, TKey> : IAggregateRootMappingConfiguration<TEntity, TKey> where TEntity : class, IAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        public Type DbContextType => typeof(DefaultDbContext);

        public Type EntityType => typeof(TEntity);

        public abstract void Map(EntityTypeBuilder<TEntity> b);

        public void Map(ModelBuilder b)
        {
            Map(b.Entity<TEntity>());
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))//判断实体中是否继承软删除接口
            {
                b.Entity<TEntity>().HasQueryFilter(x => ((ISoftDelete)x).IsDeleted == false);
            }
        }
    }
}