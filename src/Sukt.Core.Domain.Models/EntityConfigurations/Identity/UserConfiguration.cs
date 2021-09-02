using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models
{
    public class UserConfiguration : EntityMappingConfiguration<UserEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserEntity> b)
        {
            b.HasKey(x => x.Id);
            b.Property(o => o.UserName).HasMaxLength(50).IsRequired();
            b.Property(o => o.NormalizedUserName).HasMaxLength(50);
            b.Property(o => o.EmailConfirmed);
            b.Property(o => o.PhoneNumberConfirmed).IsRequired().HasDefaultValue(true);
            b.Property(o => o.TwoFactorEnabled);
            b.Property(o => o.LockoutEnabled).HasDefaultValue(false);
            b.Property(o => o.AccessFailedCount).HasDefaultValue(0);
            b.Property(o => o.ConcurrencyStamp).IsConcurrencyToken();
            b.Property(o => o.IsDeleted).HasDefaultValue(false);
            b.ToTable("User");
        }
    }
}