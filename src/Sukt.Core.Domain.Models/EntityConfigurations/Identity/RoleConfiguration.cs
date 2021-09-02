using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models
{
    public class RoleConfiguration : EntityMappingConfiguration<RoleEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<RoleEntity> b)
        {
            b.HasKey(x => x.Id);
            b.Property(o => o.Name).HasMaxLength(50).IsRequired();
            b.Property(o => o.NormalizedName).HasMaxLength(50);
            b.Property(o => o.IsAdmin).HasDefaultValue(false);
            b.Property(o => o.IsDeleted).HasDefaultValue(false);
            b.ToTable("Role");
        }
    }
}