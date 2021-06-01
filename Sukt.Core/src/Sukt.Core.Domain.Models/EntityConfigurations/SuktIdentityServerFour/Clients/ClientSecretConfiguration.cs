using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ClientSecretConfiguration : EntityMappingConfiguration<ClientSecret, Guid>
    {
        public override void Map(EntityTypeBuilder<ClientSecret> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ClientSecret");
        }
    }
}