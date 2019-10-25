using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.VO.ButtonVO;

namespace Uwl.Data.Server.ButtonServices
{
    /// <summary>
    /// 服务层按钮管理接口
    /// </summary>
    public interface IButtonServer
    {
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sysButton"></param>
        /// <returns></returns>
        Task<bool> AddButton(SysButton sysButton);
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sysButton"></param>
        /// <returns></returns>
        Task<bool> UpdateButton(SysButton sysButton);
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        Task<bool> DeleteButton(List<Guid> guids);
        /// <summary>
        /// 分页获取按钮列表
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        (List<ButtonViewMoel>, int) GetQueryByPage(ButtonQuery buttonQuery);
        /// <summary>
        /// 获取所有的按钮列表
        /// </summary>
        /// <returns></returns>
        List<SysButton> GetButtonAllList();
        /// <summary>
        /// 获取所有的按钮列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<SysButton> GetAllListById(List<Guid> guids);
    }
}
