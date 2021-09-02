using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
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