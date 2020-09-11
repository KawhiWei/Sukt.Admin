using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.Authority;
using Sukt.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Domain.Models.EntityConfigurations.Authority
{
    public class RoleMenuConfiguration : EntityMappingConfiguration<RoleMenuEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<RoleMenuEntity> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("RoleMenu");
        }
    }
}
