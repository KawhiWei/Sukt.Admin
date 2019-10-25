using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.UserInterface;

namespace Uwl.Data.EntityFramework.UserServices
{
    public class DomainUserRoleServer : CoreRepositoryBase<SysUserRole>, IUserRoleRepository
    {
        public DomainUserRoleServer(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
    }
}
