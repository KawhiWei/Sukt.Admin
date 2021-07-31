using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application;
using Sukt.Core.Shared;
using Sukt.Core.Dtos.Menu;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Description("菜单管理")]
    public class MenuController : ApiControllerBase
    {
        private readonly IMenuContract _menu;
        private readonly ILogger<MenuController> _logger = null;

        public MenuController(IMenuContract menu, ILogger<MenuController> logger)
        {
            _menu = menu;
            _logger = logger;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("创建菜单")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] MenuInputDto input)
        {
            return (await _menu.InsertAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Description("修改菜单")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync([FromBody] MenuInputDto input)
        {
            return (await _menu.UpdateAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除菜单")]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return (await _menu.DeleteAsync(id)).ToAjaxResult();
        }

        /// <summary>
        /// 获取表格菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取表格菜单列表")]
        public async Task<AjaxResult> GetMenuTableAsync()
        {
            //Console.WriteLine($"--------控制器当前线程ID{ Thread.CurrentThread.ManagedThreadId}");
            return (await _menu.GetMenuTableAsync()).ToAjaxResult();
        }

        /// <summary>
        /// 根据Id加载菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("加载菜单")]
        public async Task<AjaxResult> GetLoadFromMenuAsync(Guid id)
        {
            return (await _menu.GetLoadFromMenuAsync(id)).ToAjaxResult();
        }

        /// <summary>
        /// 根据用户角色获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("根据用户角色获取菜单")]
        public async Task<AjaxResult> GetUserMenuTreeAsync()
        {
            return (await _menu.GetUserMenuTreeAsync()).ToAjaxResult();
        }
    }
}
