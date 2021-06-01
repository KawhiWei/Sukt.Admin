using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ApiScopeClaimConfiguration : EntityMappingConfiguration<ApiScopeClaim, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiScopeClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiScopeClaim");
        }
    }
}