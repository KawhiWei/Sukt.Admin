using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.MenuServices;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 获取树形菜单
    /// </summary>
    [Route("api/GetTree")]
    [ApiController]
    public class MenuTreeController : Controller
    {
        private IMenuServer _menuServer;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="menuServer"></param>
        public MenuTreeController(IMenuServer menuServer)
        {
            _menuServer = menuServer;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TreeList")]
        //[AllowAnonymous]
        public async Task<MessageModel<RouterBar>> GetMenusTreeList(Guid userid)
        {
            var ss = await _menuServer.RouterBar(userid);
            var data = new MessageModel<RouterBar>();
            data.success = true;
            data.response = ss;
            data.msg = "获取成功";
            return data;
        }

        #region 注释代码
        //[HttpPost]
        //public void MenuRecursive(List<SysMenu> sysMenus)
        //{
        //    //var Parent = sysMenus.Where(c => c.ParentId == null).ToList();
        //    //Mapper.Initialize(cfg => cfg.CreateMap<SysMenu, MenuTreeModel>());//这个可以再.ReverseMap()双向映射
        //    //var cfgs = new MapperConfiguration(cfg => cfg.AddProfile<MyProfile>());
        //    //cfgs.CreateMapper<SysMenu, MenuTreeModel>();
        //    //将父级菜单添加到树形结构中，使用实例化出的AutoMapper对象进行映射
        //    //1：在Srartup类中去 MyMapper myMapper = new MyMapper();但是会造成内存溢出，因此注入需使用services.AddSingleton
        //    //通过控制器构造函数注入引用list.AddRange(_myMapper.ObjectMapper.Map<List<SysMenu>, IList<MenuTreeModel>>(Parent));
        //    //使用封装的静态属性进行引用AutoMapper
        //    //menuTreeModels.AddRange(MyMappers.ObjectMapper.Map<List<SysMenu>, IList<MenuTreeModel>>(Parent));
        //    //foreach (var item in menuTreeModels)
        //    //{
        //    //    var childlist=sysMenus.Where(x => x.ParentId == item.Id).ToList();
        //    //    if (childlist.Any())
        //    //    {
        //    //        item.children.AddRange(MyMappers.ObjectMapper.Map<List<SysMenu>, IList<MenuTreeModel>>(childlist));
        //    //    }
        //    //}


        //    //获取所有的第一级
        //    var Parent = sysMenus.FindAll(c => c.ParentId == null).ToList();
        //    //CreateMenuTree()
        //    //return new List <MenuTreeModel>();
        //}
        
        /// <summary>
        /// 递归循环树状结构
        /// </summary>
        /// <param name="basemenu"></param>
        /// <param name="menuTreeModel"></param>
        //[HttpPost]
        //public void CreateMenuTree(List<SysMenu> basemenu,MenuTreeModel menuTreeModel)
        //{
        //    //得到根节点下的子集
        //    var child = basemenu.FindAll(m => m.ParentId == menuTreeModel.Id);
        //    //根据根节判断是否存在子集，
        //    if (child.Any())
        //    {
        //        //存在添加到根节点下面
        //        menuTreeModel.children.AddRange(MyMappers.ObjectMapper.Map<List<SysMenu>, IList<MenuTreeModel>>(child));
        //        foreach (var item in menuTreeModel.children)
        //        {
        //            CreateMenuTree(basemenu, item);
        //        }
        //    }
        //}
        #endregion
    }
}