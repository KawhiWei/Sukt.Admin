using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class IdentityResourcePropertyConfiguration : EntityMappingConfiguration<IdentityResourceProperty, Guid>
    {
        public override void Map(EntityTypeBuilder<IdentityResourceProperty> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("IdentityResourceProperty");
        }
    }
}