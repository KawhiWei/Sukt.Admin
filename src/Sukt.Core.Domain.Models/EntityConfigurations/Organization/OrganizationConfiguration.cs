using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.Organization;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.EntityConfigurations.Organization
{
    public class OrganizationConfiguration : EntityMappingConfiguration<OrganizationEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<OrganizationEntity> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.Name).HasMaxLength(200).IsRequired();
            b.ToTable("Organization");
        }
    }
}
