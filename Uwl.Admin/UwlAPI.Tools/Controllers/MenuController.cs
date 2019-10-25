using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uwl.Common.AutoMapper;
using Uwl.Common.Utility;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Model.VO.MenuVO;
using Uwl.Data.Server.MenuServices;
using Uwl.Data.Server.RoleAssigServices;
using Uwl.Extends.Utility;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 菜单管理API接口
    /// </summary>
    [Route("api/Menus")]
    //[EnableCors("AllRequests")]
    [ApiController]
    public class MenuController : Controller
    {
        private IMenuServer _menuServer;
        /// <summary>
        /// 注入服务层
        /// </summary>
        /// <param name="menuServer">菜单管理服务层</param>
        public MenuController(IMenuServer menuServer)
        {
            _menuServer = menuServer;
        }
        /// <summary>
        /// 分页获取菜单列表
        /// </summary>
        /// <returns></returns>
        [Route("PageMenu")]
        [HttpGet]
        public MessageModel<PageModel<MenuViewMoel>> GetMenuByPage([FromQuery]MenuQuery menuQuery)
        {
            var list =  _menuServer.GetQueryMenuByPage(menuQuery, out int Total);
            var lists = _menuServer.GetQueryMenuByPage(menuQuery, out int Totals);
            //var query = (from a in list join b in lists on a.
            //             )
            return  new MessageModel<PageModel<MenuViewMoel>>()
            {
                success = true,
                msg = "数据获取成功",
                response = new PageModel<MenuViewMoel>()
                {
                    TotalCount = Total,
                    data = list,
                }
            };
        }

        /// <summary>
        /// 根据ID获取一个菜单的视图对象
        /// </summary>
        /// <param name="Id">传入的菜单ID</param>
        /// <returns></returns>
        [Route("GetMenu")]
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet]
        public async Task<SysMenu> Get(Guid Id)
        {
           return await _menuServer.GetMenu(Id);
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        // POST: api/Menu
        [Route("AddMenu")]
        [HttpPost]
        public async Task<MessageModel<string>> AddMenuPost([FromBody] SysMenu sysMenu)
        {
            var data = new MessageModel<string>();
            data.success = await _menuServer.AddMenu(sysMenu);
            if (data.success)
            {
                data.msg = "修改成功";
            }
            return data;
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        /// <returns></returns>
        [Route("MenuUpdate")]
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] SysMenu sysMenu)
        {
            var data = new MessageModel<string>();
            try
            {
                data.success = await _menuServer.UpdateMenu(sysMenu);
                if (data.success)
                {
                    data.msg = "修改成功";
                }
                return data;
            }
            catch (Exception ex)
            {
                data.success = false;
                data.msg = "修改失败"+ ex.Message;
                return data;
            }
            
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="Ids"></param>
        [Route("MenuDelete")]
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteMenu(string Ids)
        {
            //var Idss = "";
            var IdList= JsonConvert.DeserializeObject<List<Guid>>(Ids);
            var data = new MessageModel<string>();
            //List<Guid> Ids = new List<Guid>();
            try
            {
                var syslist = _menuServer.GetAllListByWhere(IdList);
                syslist.ForEach(x =>
                {
                    x.IsDrop = true;
                });
                data.success = await _menuServer.DeleteMenu(syslist);
                if (data.success)
                {
                    data.msg = "删除成功";
                }
                return data;
            }
            catch (Exception ex)
            {
                data.success = false;
                data.msg = ex.Message;
                return data; 
            }
            
        }
        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MenuList")]
        public async Task<MessageModel<List<SysMenu>>> GetAllMenuList()
        {
            var data = new MessageModel<List<SysMenu>>();
            try
            {
                data.success = true;
                data.msg = "数据获取成功";
                data.response = await _menuServer.GetMenuList();
                return data;
            }
            catch (Exception ex)
            {
                data.msg = "数据获取失败"+ex.Message;
                data.response =new List<SysMenu>();
                return data;
            }
            
        }

        ///// <summary>
        ///// 多返回值
        ///// </summary>
        ///// <returns></returns>
        //public async Task<(List<SysMenu>,double,int,int)> Getinde()
        //{
        //    return  (new List<SysMenu>(), 1.2, 1, 1);
        //}
    }
}
