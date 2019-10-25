using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.MenuInterface;

namespace Uwl.Data.EntityFramework.MenuServices
{
    public class DomainSysMenuButton : CoreRepositoryBase<SysMenuButton>, ISysMenuButton
    {
        public DomainSysMenuButton(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
    }
}
