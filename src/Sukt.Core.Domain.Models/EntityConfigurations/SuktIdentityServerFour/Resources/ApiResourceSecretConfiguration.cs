using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ApiResourceSecretConfiguration : EntityMappingConfiguration<ApiResourceSecret, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiResourceSecret> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiResourceSecret");
        }
    }
}