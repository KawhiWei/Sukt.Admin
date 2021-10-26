using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Attributes.Dependency;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Models.SeedDatas.SuktAuth
{
    [Dependency(ServiceLifetime.Singleton)]
    public class SuktAuthApplicationSeedData : SeedDataDefaults<SuktApplication, Guid>
    {
        public SuktAuthApplicationSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<SuktApplication, bool>> Expression(SuktApplication entity)
        {
            return x => x.ClientId == entity.ClientId;
        }

        protected override SuktApplication[] SetSeedData()
        {
            var secret = new List<string>() { "Sukt.Core.Admin".Sha256() };
            var suktapplication = new SuktApplication(clientId: "Sukt.Dashboard", clientName:"Sukt.Admin管理后台", clientGrantType:"password", clientSecret:secret.ToJson(),
                clientScopes: new List<string>() { "Sukt.Admin.ApiResourceScope" }.ToJson(),postLogoutRedirectUris: null,secretType: "SharedSecret",redirectUris:null,properties:null,
                protocolType:"oidc",description:null,accessTokenExpire:3600);
            return new SuktApplication[]
            {
                suktapplication
            };
        }
    }
}
