using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Domain.Models.SeedDatas;
using Sukt.Module.Core.Attributes.Dependency;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sukt.Core.IdentityServer4Store
{
    //[Dependency(ServiceLifetime.Singleton)]
    //public class IdentityServer4IdentityResourceSeedData : SeedDataDefaults<IdentityResource, Guid>
    //{
    //    public IdentityServer4IdentityResourceSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
    //    {
    //    }

    //    protected override Expression<Func<IdentityResource, bool>> Expression(IdentityResource entity)
    //    {
    //        return o => o.Name == entity.Name;
    //    }

    //    protected override IdentityResource[] SetSeedData()
    //    {
    //        List<IdentityResource> identityResources = new List<IdentityResource>();
    //        foreach (var item in Config.GetIdentityResources())
    //        {
    //            var model = item.MapTo<IdentityResource>();
    //            model.CreatedAt = DateTime.Now;
    //            model.CreatedId = Guid.Parse("c5604f31-f14c-e8be-0833-9c69b2a8eba2");
    //            model.IsDeleted = false;
    //            identityResources.Add(model);
    //        }
    //        return identityResources.ToArray();
    //    }
    //}
}