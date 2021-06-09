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
    //public class IdentityServer4ClientSeedData : SeedDataDefaults<Client, Guid>
    //{
    //    public IdentityServer4ClientSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
    //    {
    //    }

    //    protected override Expression<Func<Client, bool>> Expression(Client entity)
    //    {
    //        return o => o.ClientId == entity.ClientId;
    //    }

    //    protected override Client[] SetSeedData()
    //    {
    //        List<Client> clients = new List<Client>();
    //        foreach (var item in Config.GetClients())
    //        {
    //            var model = item.MapTo<Client>();
    //            model.CreatedAt = DateTime.Now;
    //            model.CreatedId = Guid.Parse("c5604f31-f14c-e8be-0833-9c69b2a8eba2");
    //            model.IsDeleted = false;
    //            clients.Add(model);
    //        }
    //        return clients.ToArray();
    //    }
    //}
}