using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Shared;
using System;

namespace Sukt.Core.Domain.Models
{
    public class UserRoleConfiguration : EntityMappingConfiguration<UserRoleEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserRoleEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.IsDeleted).HasDefaultValue(false);
            b.ToTable("UserRole");
        }
    }
}