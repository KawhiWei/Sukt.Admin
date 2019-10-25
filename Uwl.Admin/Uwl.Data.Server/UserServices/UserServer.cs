using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Enum;
using Uwl.Data.Server.LambdaTree;
using Uwl.Domain.IRepositories;
using Uwl.Domain.RoleInterface;
using Uwl.Domain.UserInterface;
using Uwl.Extends.Utility;

namespace Uwl.Data.Server.UserServices
{
    /// <summary>
    /// Uwl.Data.Server为服务层
    /// 用户服务层实现
    /// </summary>
    public class UserServer : IUserServer
    {
        /// <summary>
        /// 定义领域仓储层的接口对象
        /// </summary>
        private readonly IUserRepositoty _userRepositoty;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepositoty _roleRepositoty;
        private readonly IUnitofWork _unitofWork;
        public UserServer(IUserRepositoty userRepositoty, IUserRoleRepository userRoleRepository, IRoleRepositoty roleRepositoty, 
            IUnitofWork unitofWork)
        {
            _userRepositoty = userRepositoty;
            _userRoleRepository = userRoleRepository;
            _roleRepositoty = roleRepositoty;
            this._unitofWork = unitofWork;
        }
        /// <summary>
        /// 调用仓储层的方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<SysUser> CheckUser(string userName, string password)
        {
            return await _userRepositoty.FirstOrDefaultAsync(t => t.Account == userName && t.Password == password);
        }
        #region
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<SysUser> GetSysUser(Guid userid)
        {
            return await _userRepositoty.GetModelAsync(userid);
        }
        public List<SysUser> GetUserListByPage()
        {
            PageCriteria pageCriteria = new PageCriteria();
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append(" UserName=@UserName");
            pageCriteria.ParamsList.Add(new ProcParamHelp
            {
                ParamName = "@UserName",
                ParamValue = "admin",
                ParamType = "varchar(50)",
            });
            sbWhere.Append(" and PassWord=@PassWord");
            pageCriteria.ParamsList.Add(new ProcParamHelp
            {
                ParamName = "@PassWord",
                ParamValue = "123456",
                ParamType = "varchar(50)",
            });
            pageCriteria.TableName = "Users";
            pageCriteria.PrimaryKey = "Users.Id";
            pageCriteria.Fields = "Users.UserName,Users.Password,Id";
            pageCriteria.PageIndex = 1;
            pageCriteria.PageSize = 15;
            pageCriteria.OrderBySort = " UserName desc ";
            pageCriteria.Wherecondition = sbWhere.ToString();
            //return  PageHelper.GetPageByParam<SysUser>(pageCriteria).ItemsList;
            return new List<SysUser>();
        }
        /// <summary>
        /// 存储过程调用分页
        /// </summary>
        /// <param name="userQuery"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<SysUser> GetUserListByPage(UserQuery userQuery, out int Total)
        {
            var query = ExpressionBuilder.True<SysUser>();
            query = query.And(user => user.IsDrop == false);
            if(userQuery.stateEnum!= StateEnum.All)
            {
                query = query.And(user => user.AccountState == userQuery.stateEnum);
            }
            if (!userQuery.Mobile.IsNullOrEmpty())
            {
                query = query.And(user => user.Mobile.Contains(userQuery.Mobile.Trim()));
            }
            if (!userQuery.Name.IsNullOrEmpty())
            {
                query = query.And(user => user.Name.Contains(userQuery.Name.Trim()));
            }
            if (!userQuery.Account.IsNullOrEmpty())
            {
                query = query.And(user => user.Account.Contains(userQuery.Account.Trim()));
            }
            if (!userQuery.Account.IsNullOrEmpty())
            {
                query = query.And(user => user.Account.Contains(userQuery.Account.Trim()));
            }
            Total = _userRepositoty.Count(query);
            return _userRepositoty.PageBy(userQuery.PageIndex, userQuery.PageSize, query).ToList();
        }
        public List<SysUser> GetUsers(int pageIndex, int pageSize, out int total)
        {
            total = _userRepositoty.Count(t => t.Account == "admin");
            return _userRepositoty.PageBy(pageIndex,pageSize,t=>t.Account == "admin").ToList();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(SysUser sysUser)
        {
            sysUser.Id = Guid.NewGuid();
            var str=sysUser.RoleIds;
            var RoleIds = JsonConvert.DeserializeObject<List<Guid>>(sysUser.RoleIds);
            var Rolelist = new List<SysUserRole>();
            Rolelist.AddRange(RoleIds.Select(x => new SysUserRole
            {
                RoleId = x,
                CreatedId=sysUser.CreatedId,
                CreatedName=sysUser.CreatedName,
                UserIdOrDepId=sysUser.Id,
            }));
            try
            {
                _unitofWork.BeginTransaction();
                await _userRepositoty.InsertAsync(sysUser);
                await _userRoleRepository.InsertAsync(Rolelist);
                _unitofWork.Commit();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 查询出指定Id的菜单实体
        /// </summary>
        /// <param name="GuIds"></param>
        /// <returns></returns>
        public List<SysUser> GetAllListByWhere(List<Guid> sysUserIds)
        {
            return _userRepositoty.GetAll(x=> sysUserIds.Contains(x.Id)).ToList();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(SysUser sysUser)
        {
            var RoleIds = new List<Guid>();
            var deletelist = await _userRoleRepository.GetAllListAsync(x=>x.UserIdOrDepId==sysUser.Id);
            if(!sysUser.RoleIds.IsNullOrEmpty())
                RoleIds.AddRange(JsonConvert.DeserializeObject<List<Guid>>(sysUser.RoleIds));
            var Rolelist = new List<SysUserRole>();
            Rolelist.AddRange(RoleIds.Select(x => new SysUserRole
            {
                RoleId = x,
                CreatedId = sysUser.UpdateId,
                CreatedName = sysUser.UpdateName,
                UserIdOrDepId = sysUser.Id,
            }));
            try
            {
                _unitofWork.BeginTransaction();
                await _userRepositoty.UpdateAsync(sysUser);
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
        /// <summary>
        /// 批量删除对应的用户
        /// </summary>
        /// <param name="sysUsers"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(List<Guid> guids)
        {
            try
            {
                var list = GetAllListByWhere(guids);
                list.ForEach(x =>
                {
                    x.IsDrop = true;
                });
                return await _userRepositoty.UpdateAsync(list);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 根据用户ID获取该用户下面的所有角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleByUserId(Guid userId)
        {
            try
            {
                string RoleName = "";
                var RoleIds = (await _userRoleRepository.GetAllListAsync(x => x.UserIdOrDepId == userId)).Select(x => x.RoleId);//用户角色对象仓储
                var RoleList = (await _roleRepositoty.GetAllListAsync(x => RoleIds.Contains(x.Id))).Select(x => x.Id).ToArray();//角色对象仓储
                if (RoleList.Any())
                {
                    RoleName = string.Join(',', RoleList);
                }
                return RoleName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
