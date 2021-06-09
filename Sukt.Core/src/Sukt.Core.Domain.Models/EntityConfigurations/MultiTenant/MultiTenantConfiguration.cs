using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.MultiTenant;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.EntityConfigurations.MultiTenant
{
    public class MultiTenantConfiguration : EntityMappingConfiguration<MultiTenantEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<MultiTenantEntity> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("MultiTenants").HasComment("租户信息表");
        }
    }
}
