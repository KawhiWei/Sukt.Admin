using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using System;

namespace Sukt.Core.Domain.Models.EntityConfigurations.SuktAuthServer
{
    public class SuktApplicationConfiguration : EntityMappingConfiguration<SuktApplication, Guid>
    {
        public override void Map(EntityTypeBuilder<SuktApplication> b)
        {
            b.HasKey(o => o.Id);
            b.Property(x => x.ClientName);
            b.ToTable("SuktApplications");
        }
    }
}
