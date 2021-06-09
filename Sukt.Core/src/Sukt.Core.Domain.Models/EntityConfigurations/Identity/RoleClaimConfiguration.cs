using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models
{
    public class RoleClaimConfiguration : EntityMappingConfiguration<RoleClaimEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<RoleClaimEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.RoleId).IsRequired();
            b.Property(o => o.ClaimType).HasMaxLength(500);
            b.Property(o => o.ClaimValue).HasMaxLength(500);
            b.Property(o => o.IsDeleted).HasDefaultValue(false);
            b.ToTable("RoleClaim");
        }
    }
}