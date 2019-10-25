using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;

namespace Uwl.Domain.RoleInterface
{
    public interface IRoleRightAssigRepository : IRepository<SysRoleRight>
    {
        /// <summary>
        /// 添加新权限，删除old权限
        /// </summary>
        /// <param name="InsertRoleAssig">新权限数据</param>
        /// <param name="DeleteRoleAssig">旧权限数据</param>
        /// <returns></returns>
        bool SaveRoleAssigByTrans(List<SysRoleRight> InsertRoleAssig, List<SysRoleRight> DeleteRoleAssig);
    }
}
