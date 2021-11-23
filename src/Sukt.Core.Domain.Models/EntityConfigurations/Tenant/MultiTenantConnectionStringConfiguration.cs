using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.Tenant;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Models.EntityConfigurations.Tenant
{
    public class MultiTenantConnectionStringConfiguration : EntityMappingConfiguration<MultiTenantConnectionString, Guid>
    {
        public override void Map(EntityTypeBuilder<MultiTenantConnectionString> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.Name).HasMaxLength(50).IsRequired();
            b.Property(o => o.Value).HasMaxLength(400).IsRequired();
            b.Property<Guid>(o => o.TenantId).HasDefaultValue(Guid.Empty).IsRequired();
            b.ToTable("MultiTenantConnectionStrings");
        }
    }
}
