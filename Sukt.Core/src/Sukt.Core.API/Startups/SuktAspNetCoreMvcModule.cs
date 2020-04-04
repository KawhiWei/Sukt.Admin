
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktAppModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.API.Startups
{
    public class SuktAspNetCoreMvcModule: SuktAppModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            service.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            return service;
        }
        public override void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseRouting();
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
