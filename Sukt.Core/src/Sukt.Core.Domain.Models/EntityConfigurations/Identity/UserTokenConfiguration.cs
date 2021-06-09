using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models
{
    public class UserTokenConfiguration : EntityMappingConfiguration<UserTokenEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserTokenEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.IsDeleted).HasDefaultValue(false);
            b.Property(o => o.UserId);
            b.Property(o => o.LoginProvider).HasMaxLength(450);
            b.Property(o => o.Name).HasMaxLength(450);
            b.Property(o => o.Value);
            b.ToTable("UserToken");
        }
    }
}