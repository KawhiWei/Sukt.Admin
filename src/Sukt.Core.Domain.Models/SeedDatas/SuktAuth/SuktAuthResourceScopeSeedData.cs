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
    public class SuktAuthResourceScopeSeedData : SeedDataDefaults<SuktResourceScope, Guid>
    {
        public SuktAuthResourceScopeSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<SuktResourceScope, bool>> Expression(SuktResourceScope entity)
        {
            return x => x.Name == entity.Name;
        }

        protected override SuktResourceScope[] SetSeedData()
        {
            var scopelist = new List<string>() { "sukt.admin.Dashboard" };
            var suktresourcescope = new SuktResourceScope("sukt.admin.react", "通用Admin后台管理", scopelist.ToJson());

            return new SuktResourceScope[]
            {
                suktresourcescope,
            };
        }
    }
}
