using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ApiScopeConfiguration : EntityMappingConfiguration<ApiScope, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiScope> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiScope");
        }
    }
}