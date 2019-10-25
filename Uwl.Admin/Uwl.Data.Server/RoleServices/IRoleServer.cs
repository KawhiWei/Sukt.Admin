using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.Server.RoleServices
{
    /// <summary>
    /// 角色服务层接口定义
    /// </summary>
    public interface IRoleServer
    {
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        List<SysRole> GetRoleListByPage(RoleQuery roleQuery, out int Total);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        Task<bool> AddRole(SysRole sysRole);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        Task<bool> UpdateRole(SysRole sysRole);
        bool Update(SysRole sysRole);
        /// <summary>
        /// 根据角色ID获取所有的角色信息
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        List<SysRole> GetAllListById(List<Guid> guids);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        Task<bool> DeleteRole(List<Guid> guids);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <returns></returns>
        Task<List<SysRole>> GetAllListByWhere();
    }
}
