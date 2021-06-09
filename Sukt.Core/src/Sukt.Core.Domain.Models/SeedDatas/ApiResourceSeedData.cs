using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core.Attributes.Dependency;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sukt.Core.Domain.Models.SeedDatas
{
    [Dependency(ServiceLifetime.Singleton)]
    public class ApiResourceSeedData : SeedDataAggregates<ApiResource, Guid>
    {
        public ApiResourceSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<ApiResource, bool>> Expression(ApiResource entity)
        {
            return x => x.Name == entity.Name;
        }

        protected override ApiResource[] SetSeedData()
        {
            //Api资源Name是在资源服务器上验证的配置
            var adminentity = new ApiResource("Sukt.Core.API.Agile.Admin", "通用后台管理Admin敏捷开发框架");//Api资源名称添加
            adminentity.AddSecrets(new ApiResourceSecret("SuktCore.API.Admin_secret", "SharedSecret", null));//APi资源密钥
            //此处的ApiResource.Scopes必须和ApiScope内的Name相同
            adminentity.AddScopes(new List<string>() { "SuktCore.API.Admin" });//添加授权客户端访问admin框架资源范围
            return new ApiResource[]
            {
                adminentity
            };
        }
    }
}
