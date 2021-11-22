using Sukt.Core.Domain.Models;
using Sukt.Core.Identity;
using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using System;

namespace Sukt.Core.EntityFrameworkCore.Repositories
{
    public class UserStore : UserStoreBase<User, Guid, UserClaimEntity, UserLoginEntity, UserTokenEntity, RoleEntity, Guid, UserRoleEntity>
    {
        public UserStore(
            IAggregateRootRepository<User, Guid> userRepository,
            IEFCoreRepository<UserLoginEntity, Guid> userLoginRepository,
            IEFCoreRepository<UserClaimEntity, Guid> userClaimRepository,
            IEFCoreRepository<UserTokenEntity, Guid> userTokenRepository,
            IEFCoreRepository<RoleEntity, Guid> roleRepository,
            IEFCoreRepository<UserRoleEntity, Guid> userRoleRepository)
            : base(userRepository, userLoginRepository, userClaimRepository, userTokenRepository, roleRepository, userRoleRepository)
        {
        }
    }
}