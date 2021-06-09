using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using System;
using Sukt.EntityFrameworkCore.MappingConfiguration;
namespace Sukt.Core.Domain.Models.SuktIdentityServerFour
{
    public class ClientConfiguration : AggregateRootMappingConfiguration<Client, Guid>
    {
        public override void Map(EntityTypeBuilder<Client> b)
        {
            b.HasKey(o => o.Id);
            b.Property(x => x.Enabled).HasDefaultValue(true);
            b.Property(x => x.ProtocolType);
            b.Property(x => x.RequireClientSecret).HasDefaultValue(true);
            b.Property(x => x.AllowRememberConsent).HasDefaultValue(true);
            b.Property(x => x.RequirePkce).HasDefaultValue(true);
            b.Property(x => x.FrontChannelLogoutSessionRequired).HasDefaultValue(true);
            b.Property(x => x.BackChannelLogoutSessionRequired).HasDefaultValue(true);
            b.Property(x => x.IncludeJwtId).HasDefaultValue(true);
            b.Property(x => x.IdentityTokenLifetime).HasDefaultValue(300);
            b.Property(x => x.AccessTokenLifetime).HasDefaultValue(3600);
            b.Property(x => x.AuthorizationCodeLifetime).HasDefaultValue(300);
            b.Property(x => x.AbsoluteRefreshTokenLifetime).HasDefaultValue(2592000);
            b.Property(x => x.SlidingRefreshTokenLifetime).HasDefaultValue(2592000);
            b.Property(x => x.RefreshTokenUsage).HasDefaultValue(-1);
            b.Property(x => x.RefreshTokenExpiration).HasDefaultValue(-1);
            b.Property(x => x.EnableLocalLogin).HasDefaultValue(true);
            b.Property(x => x.ClientClaimsPrefix).HasDefaultValue("client_");
            b.Property(x => x.DeviceCodeLifetime).HasDefaultValue(300);
            b.ToTable("Client");
        }
    }
}
