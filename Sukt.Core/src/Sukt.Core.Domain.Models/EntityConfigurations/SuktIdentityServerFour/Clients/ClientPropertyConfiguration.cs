using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ClientPropertyConfiguration : EntityMappingConfiguration<ClientProperty, Guid>
    {
        public override void Map(EntityTypeBuilder<ClientProperty> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ClientProperty");
        }
    }
}