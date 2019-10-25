using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;

namespace Uwl.Domain.UserInterface
{
    /// <summary>
    /// 用户角色管理领域层
    /// </summary>
    public interface IUserRoleRepository : IRepository<SysUserRole>
    {
    }
}
