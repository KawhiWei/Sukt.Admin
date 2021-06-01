using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    internal class ApiResourceScopeConfiguration : EntityMappingConfiguration<ApiResourceScope, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiResourceScope> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiResourceScope");
        }
    }
}