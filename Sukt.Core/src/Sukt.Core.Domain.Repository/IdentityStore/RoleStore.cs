using Sukt.Core.Domain.Models;
using Sukt.Core.Identity;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Domain.Repository
{
    public class RoleStore : RoleStoreBase<RoleEntity, Guid, RoleClaimEntity>
    {
        public RoleStore(IEFCoreRepository<RoleEntity, Guid> roleRepository, IEFCoreRepository<RoleClaimEntity, Guid> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}
