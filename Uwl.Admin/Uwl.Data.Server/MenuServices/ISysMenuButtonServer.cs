using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.Server.MenuServices
{
    /// <summary>
    /// 菜单按钮服务层接口
    /// </summary>
    public interface ISysMenuButtonServer
    {
        /// <summary>
        /// 根据菜单Id获取已存在的按钮
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<List<Guid>> GetSysMenuButtonByMenuIdList(Guid menuId);
    }
}
