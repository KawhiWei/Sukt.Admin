using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.RoleAssigVO;
using Uwl.Domain.IRepositories;
using Uwl.Domain.UserInterface;
using Uwl.Extends.Utility;

namespace Uwl.Data.Server.UserServices
{
    /// <summary>
    /// 用户角色服务层
    /// </summary>
    public class UserRoleServer : IUserRoleServer
    {
        private readonly IUserRoleRepository  _userRoleRepository;
        private readonly IUnitofWork _unitofWork;
        public UserRoleServer(IUserRoleRepository userRoleRepository, IUnitofWork unitofWork)
        {
            _userRoleRepository = userRoleRepository;
            this._unitofWork = unitofWork;
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
        public async Task<bool> SaveRoleByUser(UpdateUserRoleVo updateUserRole)
        {
            try
            {
                var RoleIds = new List<Guid>();
                if (!updateUserRole.RoleIds.IsNullOrEmpty())
                    RoleIds.AddRange(JsonConvert.DeserializeObject<List<Guid>>(updateUserRole.RoleIds));
                var Rolelist = new List<SysUserRole>();
                var deletelist = await _userRoleRepository.GetAllListAsync(x => x.UserIdOrDepId == updateUserRole.userId);
                Rolelist.AddRange(RoleIds.Select(x => new SysUserRole
                {
                    RoleId = x,
                    CreatedId = updateUserRole.CreateId,
                    CreatedName = updateUserRole.CreateName,
                    UserIdOrDepId = updateUserRole.userId,
                }));

                _unitofWork.BeginTransaction();
                await _userRoleRepository.Delete(deletelist);
                await _userRoleRepository.InsertAsync(Rolelist);
                _unitofWork.Commit();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
