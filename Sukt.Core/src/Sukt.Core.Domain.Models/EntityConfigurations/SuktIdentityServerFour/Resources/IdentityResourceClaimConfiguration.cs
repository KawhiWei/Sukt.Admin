using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class IdentityResourceClaimConfiguration : EntityMappingConfiguration<IdentityResourceClaim, Guid>
    {
        public override void Map(EntityTypeBuilder<IdentityResourceClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("IdentityResourceClaim");
        }
    }
}