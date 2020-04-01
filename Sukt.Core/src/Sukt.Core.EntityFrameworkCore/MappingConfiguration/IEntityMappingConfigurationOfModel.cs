using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared
{
    public interface IEntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}
