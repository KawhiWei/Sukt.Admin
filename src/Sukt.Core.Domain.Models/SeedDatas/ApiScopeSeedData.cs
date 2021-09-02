using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core.Attributes.Dependency;
using System;
using System.Linq.Expressions;

namespace Sukt.Core.Domain.Models.SeedDatas
{
    [Dependency(ServiceLifetime.Singleton)]
    public class ApiScopeSeedData : SeedDataAggregates<ApiScope, Guid>
    {
        public ApiScopeSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<ApiScope, bool>> Expression(ApiScope entity)
        {
            return x => x.Name == entity.Name;
        }

        protected override ApiScope[] SetSeedData()
        {
            return new ApiScope[]
            {
                //此处的ApiScope.Name必须和ApiResourceScopes内的相同
                new ApiScope("SuktCore.API.Admin","添加授权客户端访问admin框架资源范围"),
            };
        }
    }
}
