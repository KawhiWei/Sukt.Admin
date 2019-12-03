using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Data.Model.VO.MenuVO;

namespace Uwl.Data.Server.MenuServices
{
    /// <summary>
    /// 菜单管理服务层接口定义
    /// </summary>
    public interface IMenuServer
    {
        /// <summary>
        /// 获取菜单列表，非树形
        /// </summary>
        /// <returns></returns>
        Task<List<SysMenu>> GetMenuList();
        /// <summary>
        /// 根据ID获取一个菜单的对象
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<SysMenu> GetMenu(Guid menuId);
        /// <summary>
        /// 获取树形菜单，渲染左侧菜单使用
        /// </summary>
        /// <returns></returns>
        Task<RouterBar> RouterBar(Guid userId);
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        Task<bool> AddMenu(SysMenu sysMenu);
        /// <summary>
        /// 根据表达式获取指定的数据
        /// </summary>
        /// <returns></returns>
        List<SysMenu> GetAllListByWhere(System.Collections.Generic.List<Guid> GuIds);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sysMenus"></param>
        /// <returns></returns>
        Task<bool> DeleteMenu(System.Collections.Generic.List<SysMenu> sysMenus);
        /// <summary>
        /// 查询条件分页查询出来菜单列表
        /// </summary>
        /// <param name="GuIds"></param>
        /// <returns></returns>
        (List<MenuViewMoel>, int) GetQueryMenuByPage(MenuQuery menuQuery);
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        Task<bool> UpdateMenu(SysMenu sysMenu);
        /// <summary>
        /// 重置缓存
        /// </summary>
        Task RestMenuCache();
    }
}
