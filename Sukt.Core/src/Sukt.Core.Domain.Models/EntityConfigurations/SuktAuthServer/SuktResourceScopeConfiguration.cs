using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Models.EntityConfigurations.SuktAuthServer
{
    public class SuktResourceScopeConfiguration : EntityMappingConfiguration<SuktResourceScope, Guid>
    {
        public override void Map(EntityTypeBuilder<SuktResourceScope> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("SuktResourceScopes");
        }
    }
}
