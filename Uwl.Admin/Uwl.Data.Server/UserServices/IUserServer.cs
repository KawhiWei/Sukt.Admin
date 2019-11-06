using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.VO.Personal;

namespace Uwl.Data.Server.UserServices
{
    /// <summary>
    /// 用户服务层接口
    /// </summary>
    public interface IUserServer
    {
        Task<SysUser> CheckUser(string userName, string password);
        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<SysUser> GetUsers(int pageIndex, int pageSize, out int total);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        Task<bool> AddUser(SysUser sysUser);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(SysUser sysUser);
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<SysUser> GetSysUser(Guid userid);
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        List<SysUser> GetUserListByPage(UserQuery userQuery, out int Total);
        /// <summary>
        /// 查询出指定Id的用户实体
        /// </summary>
        /// <param name="GuIds"></param>
        /// <returns></returns>
        List<SysUser> GetAllListByWhere(List<Guid> sysUserIds);
        /// <summary>
        /// 删除指定的用户
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(List<Guid> guids);
        /// <summary>
        /// 根据用户ID获取该用户下面的所有角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetUserRoleByUserId(Guid userId);
        /// <summary>
        /// 用户个人修改密码
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<bool> ChangePwd(ChangePwdVO changePwd);
        /// <summary>
        /// 用户个人资料修改
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<bool> ChangeData(ChangeDataVO changeData);
    }
}
