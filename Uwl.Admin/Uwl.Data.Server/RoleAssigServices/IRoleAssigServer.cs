using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Data.Model.RoleAssigVO;

namespace Uwl.Data.Server.RoleAssigServices
{
    public interface IRoleAssigServer
    {
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        Task<RoleAssigMenuViewModel> GetRoleAssigMenuViewModels(Guid RoleId);
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="saveRoleAssigView"></param>
        /// <returns></returns>
        Task<bool> SaveRoleAssig(SaveRoleAssigViewModel saveRoleAssigView);
        /// <summary>
        /// 在自定义策略处理器中调用方法
        /// 根据Httpcontext请求获取所属角色的所有action集合
        /// </summary>
        /// <param name="roleArr"></param>
        /// <returns></returns>
        Task<List<RoleActionModel>> GetRoleAction(Guid[] roleArr);
    }
}
