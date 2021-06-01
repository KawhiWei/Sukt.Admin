using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class PersistedGrantConfiguration : EntityMappingConfiguration<PersistedGrant, Guid>
    {
        public override void Map(EntityTypeBuilder<PersistedGrant> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("PersistedGrant");
        }
    }
}