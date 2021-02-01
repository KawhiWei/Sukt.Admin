namespace Sukt.Core.API.Config
{
    //[Dependency(ServiceLifetime.Singleton)]
    //public class ApiResourceSeedData : SeedDataAggregates<ApiResource, Guid>
    //{
    //    public ApiResourceSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
    //    {
    //    }

    //    protected override Expression<Func<ApiResource, bool>> Expression(ApiResource entity)
    //    {
    //        return x => x.Name == entity.Name;
    //    }

    //    protected override ApiResource[] SetSeedData()
    //    {
    //        //var adminentity = new ApiResource("Sukt.Core.API.Agile.Admin", "通用后台管理Admin敏捷开发框架");//Api资源名称添加
    //        //adminentity.AddSecrets(new ApiResourceSecret("SuktCore.API.Admin_secret", "SharedSecret", null));//APi资源密钥
    //        ////此处的ApiResource.Scopes必须和ApiScope内的Name相同
    //        //adminentity.AddScopes(new List<string>() { "SuktCore.API.Admin" });//添加授权客户端访问admin框架资源范围
    //        List<ApiResource> apiResources = new List<ApiResource>();
    //        foreach (var item in Config.GetApiResources())
    //        {
    //            var model = item.MapTo<ApiResource>();
    //            apiResources.Add(model);
    //        }
    //        return apiResources.ToArray();
    //        //return new ApiResource[]
    //        //{
    //        //    adminentity
    //        //};
    //    }
    //}
}
