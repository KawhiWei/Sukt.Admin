using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.AutoMapper;
using Uwl.Common.Cache.RedisCache;
using Uwl.Common.LambdaTree;
using Uwl.Common.Sort.SortEnum;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Data.Model.VO.ButtonVO;
using Uwl.Data.Model.VO.MenuVO;
using Uwl.Domain.ButtonInterface;
using Uwl.Domain.MenuInterface;
using Uwl.Domain.RoleInterface;
using Uwl.Domain.UserInterface;
using Uwl.Extends.Sort;
using Uwl.Extends.Utility;

namespace Uwl.Data.Server.MenuServices
{
    /// <summary>
    /// 菜单管理服务层实现
    /// </summary>
    public class MenuServer : IMenuServer
    {
        private readonly IMenuRepositoty _menuRepositoty;
        private readonly IButtonRepositoty _buttonRepositoty;
        private readonly IUserRoleRepository _userRoleRepository;//用户角色仓储层接口定义
        private readonly IRoleRightAssigRepository _roleRightAssigRepository;//定义角色权限领域层对象
        private readonly IRedisCacheManager _redisCacheManager;
        /// <summary>
        /// 通过构造函数注入领域仓储层的接口
        /// </summary>
        /// <param name="menuRepositoty"></param>
        public MenuServer(
            IMenuRepositoty menuRepositoty, 
            IButtonRepositoty buttonRepositoty,
            IUserRoleRepository userRoleRepository,
            IRoleRightAssigRepository roleRightAssigRepository, IRedisCacheManager redisCacheManager
            )
        {
            this._menuRepositoty = menuRepositoty;
            this._buttonRepositoty = buttonRepositoty;
            this._userRoleRepository = userRoleRepository;
            this._roleRightAssigRepository = roleRightAssigRepository;
            this._redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// 根据ID获取一个菜单的对象
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<SysMenu> GetMenu(Guid menuId)
        {
           return await _menuRepositoty.GetModelAsync(menuId);
        }
        /// <summary>
        /// 获取菜单列表，非树形
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysMenu>> GetMenuList()
        {
            return await this._menuRepositoty.GetAllListAsync(x=>x.IsDrop==false);
        }
        /// <summary>
        /// 获取树形菜单，渲染左侧菜单使用
        /// </summary>
        /// <returns></returns>
        public async Task<RouterBar> RouterBar(Guid userId)
        {
            //记录Job时间
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            var Rolelist = await _userRoleRepository.GetAllListAsync(x => x.UserIdOrDepId == userId);//根据用户ID获取角色列表
            var RoleIds=Rolelist.Select(x => x.RoleId).ToList();//获取到角色列表中的角色ID
            var MenuList = await _roleRightAssigRepository.GetAllListAsync(x => RoleIds.Contains(x.RoleId));//根据角色Id获取菜单列表
            var MenuIds = MenuList.Select(x => x.MenuId).ToList();//获取角色权限中的菜单ID

            //stopwatch.Stop();

            //string msg = "我是测试定时任务发起的消息队列本次任务执行时间：" + stopwatch.Elapsed.TotalMilliseconds.ToString();
            //_redisCacheManager.PublishAsyncRedis("testmes", msg);
            //_redisCacheManager.PublishAsyncRedis("testmes", msg);

            List<SysMenu> baselist = new List<SysMenu>();
            if (_redisCacheManager.Get("Menu"))//判断菜单缓存是否存在，如果存在则取缓存不存在则取数据库
            {
                baselist = _redisCacheManager.GetList<SysMenu>("Menu").Where(x=>MenuIds.Contains(x.Id)).ToList();
            }
            else
            {
                await RestMenuCache();
                baselist = _redisCacheManager.GetList<SysMenu>("Menu").Where(x => MenuIds.Contains(x.Id)).ToList();
            }
            if (!baselist.Any())
            {
                baselist = await _menuRepositoty.GetAllListAsync(x => MenuIds.Contains(x.Id));//根据菜单ID获取菜单列表 x=>MenuIds.Contains(x.Id)
            }
            var btnlist = await _buttonRepositoty.GetAllListAsync(x => x.IsShow == true && x.IsDrop == false);
            RouterBar routerbar = new RouterBar()
            {
                Id = Guid.Empty,
                name = "根节点",
                order = 0,
                path = "",
                meta = new NavigationBarMeta(),
                ParentId = Guid.Empty,
            };
            //定义一个树形列表
            var routerbarTreeall = baselist.Where(x => x.IsDrop == false).Select(s => new RouterBar
            {
                Id=s.Id,
                name=s.Name,
                ParentId=s.ParentId,
                iconCls=s.Icon,
                order=s.Sort,
                path=s.UrlAddress,
                meta=new NavigationBarMeta
                {
                    requireAuth=true,
                    title=s.Name,
                }
            }).ToList();
            try
            {
                //递归形成前端路由器格式的树形菜单
                CreateMenuTree(routerbarTreeall, routerbar, btnlist, MenuList);
                return routerbar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        public async Task<bool> AddMenu(SysMenu sysMenu)
        {
            var result = await _menuRepositoty.InsertAsync(sysMenu);
            //await RestMenuCache();//菜单进行修改重置缓存
            return result;
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        public async Task<bool> UpdateMenu(SysMenu sysMenu)
        {
            sysMenu.UpdateDate = DateTime.Now;
            //var model = await _menuRepositoty.GetModelAsync(sysMenu.Id);
            var result=await _menuRepositoty.UpdateNotQueryAsync(sysMenu, x => x.Name, x => x.APIAddress,
                x => x.UrlAddress, x => x.ParentId, x => x.Sort, x => x.Memo,
                x => x.Icon, x => x.UpdateDate, x => x.UpdateId, x => x.ParentIdArr) > 0;
            var menus = await _menuRepositoty.GetAllListAsync(x => x.IsDrop == false);
            //_redisCacheManager.Remove("Menu");
            //_redisCacheManager.Set("Menu", menus);

            //await RestMenuCache();//菜单进行修改重置缓存
            return result;
            //return await _menuRepositoty.UpdateAsync(model);
        }
        /// <summary>
        /// 查询出指定Id的菜单实体
        /// </summary>
        /// <param name="GuIds"></param>
        /// <returns></returns>
        public List<SysMenu> GetAllListByWhere(List<Guid> MenuIds)
        {
            return _menuRepositoty.GetAll(x=> MenuIds.Contains(x.Id)).ToList();
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sysMenus"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMenu(List<SysMenu> sysMenus)
        {
            var result= await _menuRepositoty.UpdateAsync(sysMenus);
            //await RestMenuCache();//菜单进行修改重置缓存
            return result;
        }
        /// <summary>
        /// 查询条件分页查询出来菜单列表
        /// </summary>
        /// <param name="GuIds"></param>
        /// <returns></returns>
        public (List<MenuViewMoel>,int) GetQueryMenuByPage(MenuQuery menuQuery)
        {
            var query = ExpressionBuilder.True<SysMenu>();
            query = query.And(menu => menu.IsDrop == false);
            if (!menuQuery.Name.IsNullOrEmpty())
            {
                query = query.And(m => m.Name.Contains(menuQuery.Name.Trim()));
            }
            if (!menuQuery.UrlAddress.IsNullOrEmpty())
            {
                query = query.And(m => m.UrlAddress.Contains(menuQuery.UrlAddress.Trim()));
            }
            if (!menuQuery.APIAddress.IsNullOrEmpty())
            {
                query = query.And(m => m.APIAddress.Contains(menuQuery.APIAddress.Trim()));
            }
            var list = (from a in _menuRepositoty.GetAll(query)
                        join b in _menuRepositoty.GetAll(x => x.IsDrop == false) on a.ParentId equals b.Id into le from aa in le.DefaultIfEmpty() 
                        select new MenuViewMoel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ParentId =a.ParentId,
                            ParentName = aa.Name,
                            Memo = a.Memo,
                            APIAddress = a.APIAddress,
                            UrlAddress = a.UrlAddress,
                            Sort = a.Sort,
                            Icon = a.Icon,
                            CreatedDate=a.CreatedDate,
                            ParentIdArr=a.ParentIdArr,
                        });
            //添加排序
            OrderCondition<MenuViewMoel>[] orderConditions = new OrderCondition<MenuViewMoel>[]
            {
                new OrderCondition<MenuViewMoel>(x=>x.CreatedDate,SortDirectionEnum.Descending),
                new OrderCondition<MenuViewMoel>(x=>x.UrlAddress,SortDirectionEnum.Ascending)
            };
            Parameters parameters = new Parameters();
            parameters.PageIndex = menuQuery.PageIndex;
            parameters.PageSize = menuQuery.PageSize;

            parameters.OrderConditions = orderConditions;
            return (list.PageBy(parameters).ToList(),list.Count()); //_menuRepositoty.PageBy<SysMenu>(menuQuery.PageIndex, menuQuery.PageSize, query).ToList();
        }

        /// <summary>
        /// 菜单表任何操作将缓存重置
        /// </summary>
        public async Task RestMenuCache()
        {
            try
            {
                _redisCacheManager.Remove("Menu");
                var list=await this._menuRepositoty.GetAll(x=>x.IsDrop==false).AsNoTracking().ToListAsync();
                _redisCacheManager.Set("Menu", list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        #region 帮助方法
        /// <summary>
        /// 递归循环树状结构
        /// </summary>
        /// <param name="basemenu"></param>
        /// <param name="menuTreeModel"></param>
        //[HttpPost]
        public void CreateMenuTree(List<RouterBar> all, RouterBar curItem,List<SysButton> sysButtons,List<SysRoleRight> sysRoleRights)
        {
            //得到根节点下的子集
            var child = all.FindAll(m => m.ParentId == curItem.Id);
            //根据根节判断是否存在子集，
            if (child.Any())
            {
                curItem.children = new List<RouterBar>();
                curItem.children.AddRange(child);
            }
            else
            {
                curItem.children = new List<RouterBar>();
            }
            foreach (var item in child)
            {
                var roles= sysRoleRights.Where(x => x.MenuId == item.Id).FirstOrDefault();
                if (roles != null)
                {
                    //判断改角色的菜单下是否存在按钮ID，
                    if (!roles.ButtonIds.IsNullOrEmpty())
                    {
                        var btnIds= roles.ButtonIds.Split(',').Select(x=>x.ToGuid()).ToList();//获取Id的字符串
                        //查询出拥有权限的按钮
                        var btn = sysButtons.Where(x => btnIds.Contains(x.Id)).ToList().Select(x => new BtnIsDisplayViewModel
                        {
                            Name = x.Name,
                            BtnStyle = x.ButtonStyle,
                            KeyCode = x.KeyCode
                        }).ToList();
                        item.btnIsDisplayViews = btn;//将按钮添加到菜单下面
                    }
                }
                CreateMenuTree(all, item, sysButtons, sysRoleRights);
            }
        }
        #endregion
    }
}
