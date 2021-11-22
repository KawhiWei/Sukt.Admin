using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.Tenant;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.EntityConfigurations.Tenant
{
    public class MultiTenantConfiguration : AggregateRootMappingConfiguration<MultiTenant, Guid>
    {
        public override void Map(EntityTypeBuilder<MultiTenant> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("MultiTenants").HasComment("租户信息表");
        }
    }
}
