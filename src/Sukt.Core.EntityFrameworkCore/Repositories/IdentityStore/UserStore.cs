using Sukt.Core.Domain.Models;
using Sukt.Core.Identity;
using Sukt.Module.Core.Entity;
using System;

namespace Sukt.Core.EntityFrameworkCore.Repositories
{
    public class UserStore : UserStoreBase<UserEntity, Guid, UserClaimEntity, UserLoginEntity, UserTokenEntity, RoleEntity, Guid, UserRoleEntity>
    {
        public UserStore(
            IEFCoreRepository<UserEntity, Guid> userRepository,
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