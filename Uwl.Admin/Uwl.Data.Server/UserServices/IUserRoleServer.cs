using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.RoleAssigVO;

namespace Uwl.Data.Server.UserServices
{
    public interface IUserRoleServer
    {
        /// <summary>
        /// 根据用户ID获取已有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Guid>> GetRoleIdListByUserId(Guid userId);
        /// <summary>
        /// 用户分配角色
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveRoleByUser(UpdateUserRoleVo updateUserRole);
    }
}
