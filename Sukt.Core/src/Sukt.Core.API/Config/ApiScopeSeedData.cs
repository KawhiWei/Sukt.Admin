namespace Sukt.Core.API.Config
{
    //[Dependency(ServiceLifetime.Singleton)]
    //public class ApiScopeSeedData : SeedDataAggregates<ApiScope, Guid>
    //{
    //    public ApiScopeSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
    //    {
    //    }

    //    protected override Expression<Func<ApiScope, bool>> Expression(ApiScope entity)
    //    {
    //        return x => x.Name == entity.Name;
    //    }

    //    protected override ApiScope[] SetSeedData()
    //    {

    //        //return new ApiScope[]
    //        //{
    //        //    //此处的ApiScope.Name必须和ApiResourceScopes内的相同
    //        //    new ApiScope("SuktCore.API.Admin","添加授权客户端访问admin框架资源范围"),
    //        //};
    //        List<ApiScope> apiScope = new List<ApiScope>();
    //        foreach (var item in Config.GetApiResources())
    //        {
    //            var model = item.MapTo<ApiScope>();
    //            apiScope.Add(model);
    //        }
    //        return apiScope.ToArray();
    //    }
    //}
}
