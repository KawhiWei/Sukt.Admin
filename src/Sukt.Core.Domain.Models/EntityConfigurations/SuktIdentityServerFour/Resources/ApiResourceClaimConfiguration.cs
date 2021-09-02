using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using System;


namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ApiResourceClaimConfiguration : Sukt.EntityFrameworkCore.MappingConfiguration.EntityMappingConfiguration<ApiResourceClaim, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiResourceClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiResourceClaim");
        }
    }
}