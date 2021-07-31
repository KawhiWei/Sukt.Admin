using Sukt.Core.Domain.Models;
using Sukt.Core.Identity;
using Sukt.Module.Core.Entity;
using System;

namespace Sukt.Core.EntityFrameworkCore.Repositories
{
    public class RoleStore : RoleStoreBase<RoleEntity, Guid, RoleClaimEntity>
    {
        public RoleStore(IEFCoreRepository<RoleEntity, Guid> roleRepository, IEFCoreRepository<RoleClaimEntity, Guid> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}