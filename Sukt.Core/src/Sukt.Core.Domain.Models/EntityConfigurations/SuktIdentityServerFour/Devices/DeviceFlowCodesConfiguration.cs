using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Shared;
using System;

namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class DeviceFlowCodesConfiguration : EntityMappingConfiguration<DeviceFlowCodes, Guid>
    {
        public override void Map(EntityTypeBuilder<DeviceFlowCodes> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("DeviceFlowCodes");
        }
    }
}