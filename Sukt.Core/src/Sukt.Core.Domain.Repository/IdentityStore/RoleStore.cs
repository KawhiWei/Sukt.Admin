using Sukt.Core.Domain.Models;
using Sukt.Core.Identity;
using SuktCore.Shared.Entity;
using System;

namespace Sukt.Core.Domain.Repository
{
    public class RoleStore : RoleStoreBase<RoleEntity, Guid, RoleClaimEntity>
    {
        public RoleStore(IEFCoreRepository<RoleEntity, Guid> roleRepository, IEFCoreRepository<RoleClaimEntity, Guid> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}