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
    //public class IdentityServer4ApiScopeSeedData : SeedDataDefaults<ApiScope, Guid>
    //{
    //    public IdentityServer4ApiScopeSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
    //    {
    //    }

    //    protected override Expression<Func<ApiScope, bool>> Expression(ApiScope entity)
    //    {
    //        return o => o.Name == entity.Name;
    //    }

    //    protected override ApiScope[] SetSeedData()
    //    {
    //        List<ApiScope> apiScopes = new List<ApiScope>();
    //        foreach (var item in Config.GetApiScopes())
    //        {
    //            var model = item.MapTo<ApiScope>();
    //            model.CreatedAt = DateTime.Now;
    //            model.CreatedId = Guid.Parse("c5604f31-f14c-e8be-0833-9c69b2a8eba2");
    //            model.IsDeleted = false;
    //            apiScopes.Add(model);
    //        }
    //        return apiScopes.ToArray();
    //    }
    //}
}