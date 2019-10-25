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
using Uwl.Extends.Utility;

namespace Uwl.Data.Server.RoleServices
{
    /// <summary>
    /// 角色服务层接口实现
    /// </summary>
    public class RoleServer:IRoleServer
    {
        private readonly IRoleRepositoty _roleRepositoty;
        private readonly IUnitofWork _unitofWork;
        public RoleServer(IRoleRepositoty roleRepositoty, IUnitofWork unitofWork)
        {
            _roleRepositoty = roleRepositoty;
            this._unitofWork = unitofWork;
        }
        /// <summary>
        /// 分页获取角色列表实现
        /// </summary>
        /// <param name="roleQuery"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<SysRole> GetRoleListByPage(RoleQuery roleQuery, out int Total)
        {
            var query = ExpressionBuilder.True<SysRole>();
            query = query.And(m => m.IsDrop == false);
            if (roleQuery.stateEnum != StateEnum.All)
            {
                query = query.And(m => m.RoletState == roleQuery.stateEnum);
            }
            if (!roleQuery.Name.IsNullOrEmpty())
            {
                query = query.And(m => m.Name.Contains(roleQuery.Name.Trim()));
            }
            Total = _roleRepositoty.Count(query);
            return _roleRepositoty.PageBy(roleQuery.PageIndex, roleQuery.PageSize, query).ToList();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        public async Task<bool> AddRole(SysRole sysRole)
        {
            return await _roleRepositoty.InsertAsync(sysRole);
        }
        /// <summary>
        /// 根据角色ID获取所有的角色信息
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        public List<SysRole> GetAllListById(List<Guid> guids)
        {
            if(guids.Count>0)
            {
                return _roleRepositoty.GetAll(x => guids.Contains(x.Id)).ToList();
            }
            return _roleRepositoty.GetAll().ToList();
        }
        public  async Task<List<SysRole>> GetAllListByWhere()
        {
            var query = ExpressionBuilder.True<SysRole>();
            query = query.And(role => role.IsDrop == false);
            return await _roleRepositoty.GetAllListAsync(query);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRole(SysRole sysRole)
        {
            sysRole.UpdateDate =DateTime.Now;
            return await _roleRepositoty.UpdateNotQueryAsync
                (sysRole,o=>o.Name,o=>o.Memo,x=>x.RoletState,x=>x.UpdateDate,x=>x.UpdateName, x => x.UpdateId) >0;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRole(List<Guid> guids)
        {
            try
            {
                var list = GetAllListById(guids);
                list.ForEach(x =>
                {
                    x.IsDrop = true;
                });
                return await _roleRepositoty.UpdateAsync(list);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(SysRole sysRole)
        {
            return _roleRepositoty.Update(sysRole);
        }
    }
}
