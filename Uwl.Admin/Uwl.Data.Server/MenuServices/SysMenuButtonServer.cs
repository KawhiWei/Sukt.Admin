using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.MenuInterface;

namespace Uwl.Data.Server.MenuServices
{
    /// <summary>
    /// 菜单按钮服务层实现
    /// </summary>
    public class SysMenuButtonServer : ISysMenuButtonServer
    {
        private ISysMenuButton _sysMenuButton;
        public SysMenuButtonServer(ISysMenuButton sysMenuButton)
        {
            this._sysMenuButton = sysMenuButton;
        }
        /// <summary>
        /// 根据菜单Id获取已存在的按钮
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetSysMenuButtonByMenuIdList(Guid menuId)
        {
            var list = await this._sysMenuButton.GetAllListAsync(x => x.MenuId == menuId);
            return list.Select(x => x.ButtonId).ToList();
        }
    }
}
