using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Attributes.Dependency;
using System;
using System.Linq.Expressions;

namespace Sukt.Core.Domain.Models.SeedDatas
{
    [Dependency(ServiceLifetime.Singleton)]
    public class UserSeedData : SeedDataDefaults<UserEntity, Guid>
    {
        public UserSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<UserEntity, bool>> Expression(UserEntity entity)
        {
            return x => x.UserName == entity.UserName && x.NormalizedUserName == entity.NormalizedUserName && x.NickName == entity.NickName;
        }

        protected override UserEntity[] SetSeedData()
        {
            return new UserEntity[] {
                new UserEntity()
                {
                    Id = Guid.Parse("c5604f31-f14c-e8be-0833-9c69b2a8eba2"),
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    NickName = "管理员",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEEPWhHPCHU1i6Z0ayoApKGbPlZUb38RUdJg4QjUcccVhUSto0sRZtLOXfwWUJ+P2Xw==",
                    SecurityStamp = "3OWMGQAK5ZTXMSV6OFSGIWWWNIWJ2SX6",
                    ConcurrencyStamp = "0286cab6-8a4a-44ed-9a97-86b0506c65c3",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsSystem = true,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    Sex = "男"
                }
            };
        }
    }
}