using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using Sukt.Module.Core.Attributes.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sukt.Core.Domain.Models.SeedDatas
{
    [Dependency(ServiceLifetime.Singleton)]
    public class IdentityResourceSeedData : SeedDataAggregates<IdentityResource, Guid>
    {
        public IdentityResourceSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<IdentityResource, bool>> Expression(IdentityResource entity)
        {
            return x => x.Name == entity.Name;
        }

        protected override IdentityResource[] SetSeedData()
        {
            ///使用方式参考IdentityServer4中的IdentityResources源码，这个不需要改动太多可做添加修改页面也可不做
            var openIdentity = new IdentityResource(IdentityServerConstants.StandardScopes.OpenId, "Your user identifier", true);
            openIdentity.AddUserClaims(new List<string> { JwtClaimTypes.Subject });
            ////
            var profileentity = new IdentityResource(IdentityServerConstants.StandardScopes.Profile, "User profile", false);
            profileentity.AddUserClaims(Constants.ScopeToClaimsMapping[IdentityServerConstants.StandardScopes.Profile].ToList());
            profileentity.SetEmphasize(true);
            return new IdentityResource[]
            {
                openIdentity,
                profileentity
            };
        }
    }
}
