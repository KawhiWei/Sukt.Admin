using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Attributes.Dependency;
using System;
using System.Linq.Expressions;

namespace Sukt.Core.Domain.Models.SeedDatas
{
    [Dependency(ServiceLifetime.Singleton)]
    public class RoleSeedData : SeedDataDefaults<RoleEntity, Guid>
    {
        public RoleSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<RoleEntity, bool>> Expression(RoleEntity entity)
        {
            return x => x.Name == entity
            .Name;
        }

        protected override RoleEntity[] SetSeedData()
        {
            return new RoleEntity[]{  new RoleEntity()
            {
                Id = Guid.Parse("81ba489a-5e52-2a49-48ec-75dc0b3f9ff2"),
                Name = "系统管理员",
                NormalizedName = "系统管理员",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedId = Guid.Parse("c5604f31-f14c-e8be-0833-9c69b2a8eba2"),
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                IsAdmin = true
            }};
        }
    }
}
