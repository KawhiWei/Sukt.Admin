using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using Sukt.Module.Core;
using System;
namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ApiResourceConfiguration : AggregateRootMappingConfiguration<ApiResource, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiResource> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.Enabled).HasDefaultValue(true);
            b.Property(o => o.ShowInDiscoveryDocument).HasDefaultValue(true);
            b.ToTable("ApiResource");
        }
    }
}
