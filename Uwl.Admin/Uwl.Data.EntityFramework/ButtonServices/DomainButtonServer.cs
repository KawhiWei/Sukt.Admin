using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.ButtonInterface;
using Uwl.Domain.IRepositories;

namespace Uwl.Data.EntityFramework.ButtonServices
{
    /// <summary>
    /// 仓储层按钮管理接口实现
    /// </summary>
    public class DomainButtonServer : CoreRepositoryBase<SysButton>, IButtonRepositoty
    {
        public readonly Domain.IRepositories.IRepository _repository;
        public DomainButtonServer(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
    }
}
