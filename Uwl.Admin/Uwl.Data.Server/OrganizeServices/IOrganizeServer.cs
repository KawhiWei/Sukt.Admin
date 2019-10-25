using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.OrganizeVO;

namespace Uwl.Data.Server.OrganizeServices
{
    /// <summary>
    /// 组织机构服务层接口
    /// </summary>
    public interface IOrganizeServer
    {
        /// <summary>
        /// 添加组织机构
        /// </summary>
        /// <param name="sysOrganize"></param>
        /// <returns></returns>
        Task<bool> AddOrganize(SysOrganize sysOrganize);
        /// <summary>
        /// 修改组织机构
        /// </summary>
        /// <param name="sysOrganize"></param>
        /// <returns></returns>
        Task<bool> UpdateOrganize(SysOrganize sysOrganize);
        /// <summary>
        /// 分页获取组织机构列表
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        (List<SysOrganize>,int) GetOrganizePage(BaseQuery baseQuery);
        /// <summary>
        /// 获取所有的组织机构
        /// </summary>
        /// <returns></returns>
        Task<OrganizeViewModel> GetAll();
        /// <summary>
        /// 通过ID获取一个组织机构对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<SysOrganize> GetOrganize(Guid Id);
        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        Task<bool> DeleteOrganize(List<Guid> guids);

    }
}
