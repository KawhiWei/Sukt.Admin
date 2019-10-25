using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Domain.UserInterface;

namespace Uwl.Data.Server.UserServices
{
    /// <summary>
    /// 用户角色服务层
    /// </summary>
    public class UserRoleServer : IUserRoleServer
    {
        private readonly IUserRoleRepository  _userRoleRepository;
        public UserRoleServer(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        /// <summary>
        /// 根据用户ID获取已有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetRoleIdListByUserId(Guid userId)
        {
            var list=await _userRoleRepository.GetAllListAsync(x => x.UserIdOrDepId == userId);
            return list.Select(x=>x.RoleId).ToList();

        }
    }
}
