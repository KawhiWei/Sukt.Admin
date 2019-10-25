using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.OrganizeInterface;

namespace Uwl.Data.EntityFramework.OrganizeServives
{
    public class DomainOrganizeServer : CoreRepositoryBase<SysOrganize>, IOrganizeRepositoty
    {
        /// <summary>
        /// 注入接口上下文对象
        /// </summary>
        /// <param name="uwlDbContext"></param>
        public DomainOrganizeServer(IUnitofWork unitofWork) : base(unitofWork)
        {
        }
    }
}
