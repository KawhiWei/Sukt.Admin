using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.RoleInterface;

namespace Uwl.Data.EntityFramework.UserServices
{
    /// <summary>
    /// 角色管理领域层
    /// 定义一个用户的接口实现分别继承基类实现和用户接口
    /// </summary>
    public class DomainRoleServer : CoreRepositoryBase<SysRole>, IRoleRepositoty
    {
        public DomainRoleServer(IUnitofWork unitofWork) :base(unitofWork)
        {

        }
    }
}
