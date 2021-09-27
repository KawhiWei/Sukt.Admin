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
            var secret = new List<string>() { "test" };
            var suktapplication = new SuktApplication("sukt.admin", "Sukt.Admin管理后台","password", secret.ToJson(), new List<string>() { "sukt.admin.Dashboard" }.ToJson());
            return new SuktApplication[]
            {
                suktapplication
            };
        }
    }
}
