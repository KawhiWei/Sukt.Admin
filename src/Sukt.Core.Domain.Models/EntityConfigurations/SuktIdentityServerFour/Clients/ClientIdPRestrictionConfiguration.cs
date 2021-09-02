using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ClientIdPRestrictionConfiguration : EntityMappingConfiguration<ClientIdPRestriction, Guid>
    {
        public override void Map(EntityTypeBuilder<ClientIdPRestriction> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ClientIdPRestriction");
        }
    }
}