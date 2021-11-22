using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.Models.Identity.Enum;
using Sukt.Module.Core.Attributes.Dependency;
using System;
using System.Linq.Expressions;

namespace Sukt.Core.Domain.Models.SeedDatas
{
    [Dependency(ServiceLifetime.Singleton)]
    public class UserSeedData : SeedDataAggregates<User, Guid>
    {
        public UserSeedData(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<User, bool>> Expression(User entity)
        {
            return x => x.UserName == entity.UserName && x.NormalizedUserName == entity.NormalizedUserName && x.NickName == entity.NickName;
        }

        protected override User[] SetSeedData()
        {
            return new User[] {
                new User(DateTime.Now,"博士后","教授","",true,"董事长","总经办",
                UserTypeEnum.SuperAdmin,"Admin","ADMIN","管理员","","",false,
                "AQAAAAEAACcQAAAAEEPWhHPCHU1i6Z0ayoApKGbPlZUb38RUdJg4QjUcccVhUSto0sRZtLOXfwWUJ+P2Xw==","","3OWMGQAK5ZTXMSV6OFSGIWWWNIWJ2SX6",
                "0286cab6-8a4a-44ed-9a97-86b0506c65c3","",false,false,null,true,0,true,"男")
                //{
                    //Id = Guid.Parse("c5604f31-f14c-e8be-0833-9c69b2a8eba2"),
                    //UserName = "Admin",
                    //NormalizedUserName = "ADMIN",
                    
                    //EmailConfirmed = false,
                    //PasswordHash = 
                    //SecurityStamp = "3OWMGQAK5ZTXMSV6OFSGIWWWNIWJ2SX6",
                    //ConcurrencyStamp = "0286cab6-8a4a-44ed-9a97-86b0506c65c3",
                    //PhoneNumberConfirmed = false,
                    //TwoFactorEnabled = false,
                    //LockoutEnabled = true,
                    //AccessFailedCount = 0,
                    //IsSystem = true,
                    //CreatedAt = DateTime.Now,
                    //IsDeleted = false,
                    //Sex = "男"
                //}
            };
        }
    }
}