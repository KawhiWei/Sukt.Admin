

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
    public class ClientSeedData : SeedDataAggregates<Client, Guid>
    {
        public ClientSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<Client, bool>> Expression(Client entity)
        {
            return x => x.ClientId == entity.ClientId;
        }

        protected override Client[] SetSeedData()
        {
            var entity = new Client("Sukt.Core.ReactAdmin.Spa", "Sukt.Core、React的单页面敏捷开发框架", true, true);
            entity.AddClientScopes(new List<string>() { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "SuktCore.API.Admin", });//添加授权范围
            entity.AddClientSecrets(new ClientSecret("", "Sukt.Core.ReactAdmin.Spa.Secret", "SharedSecret", null));//添加令牌
            entity.AddCorsOrigins(new List<string>() { "http://localhost:6017" });//添加IdentityServer4客户端跨域 //上线版本 "https://suktadmin.destinycore.club" 
            entity.AddPostLogoutRedirectUris(new List<string>() { "http://localhost:6017" });//添加IdentityServer4客户端退出登录跳转Url
            entity.AddRedirectUris(new List<string>() { "http://localhost:6017/callback" });//添加令牌
            entity.AddGrantTypes(GrantTypes.Implicit.ToList());//添加授权类型
            return new Client[]
            {
                entity
            };
        }
    }
}
