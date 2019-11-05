using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.OrganizeVO;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.OrganizeServices;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 组织机构API接口
    /// </summary>
    [Route("api/Organize")]
    [ApiController]
    public class OrganizeController : ControllerBase
    {
        private readonly IOrganizeServer _organizeServer;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="organizeServer"></param>
        public OrganizeController(IOrganizeServer organizeServer)
        {
            this._organizeServer = organizeServer;
        }
        /// <summary>
        /// 分页获取组织机构列表
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        [Route("GetOrganizePage")]
        [HttpGet]
        public MessageModel<PageModel<SysOrganize>> GetOrganizePage([FromQuery]BaseQuery baseQuery)
        {
            var data =new  MessageModel<PageModel<SysOrganize>>();
            try
            {
                var list=this._organizeServer.GetOrganizePage(baseQuery);
                data.success = true;
                data.msg = "数据获取成功";
                data.response.data = list.Item1;
                data.response.TotalCount = list.Item2;
                return data;
            }
            catch (Exception ex)
            {
                data.msg = ex.Message;
                return data;
            }
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrganizeTree")]
        //[AllowAnonymous]
        public async Task<MessageModel<OrganizeViewModel>> GetMenusTreeList()
        {
            var data = new MessageModel<OrganizeViewModel>();
            try
            {
                var tree = await _organizeServer.GetAll();
                data.success = true;
                data.response = tree;
                data.msg = "获取成功";
                return data;
            }
            catch (Exception)
            {
                data.response = new OrganizeViewModel();
                data.msg = "获取失败";
                return data;
            }
           
        }
        /// <summary>
        /// 添加组织机构
        /// </summary>
        /// <param name="sysOrganize"></param>
        /// <returns></returns>
        [Route("AddOrganize")]
        [HttpPost]
        public async Task<MessageModel<string>> AddOrganize([FromBody]SysOrganize sysOrganize)
        {
            var data = new MessageModel<string>();
            try
            {
                data.success = await this._organizeServer.AddOrganize(sysOrganize);
                if(data.success)
                {
                    data.msg = "组织机构添加成功";
                    return data;
                }
                else
                {
                    data.msg = "组织机构添加失败";
                    return data;
                }
            }
            catch (Exception ex)
            {
                data.msg = ex.Message;
                return data;
            }
        }
        /// <summary>
        /// 修改组织机构
        /// </summary>
        /// <param name="sysOrganize"></param>
        /// <returns></returns>
        [Route("UpdateOrganize")]
        [HttpPut]
        public async Task<MessageModel<string>> UpdateOrganize([FromBody]SysOrganize sysOrganize)
        {
            var data = new MessageModel<string>();
            try
            {
                data.success = await this._organizeServer.UpdateOrganize(sysOrganize);
                if (data.success)
                {
                    data.msg = "组织机构修改成功";
                    return data;
                }
                else
                {
                    data.msg = "组织机构修改失败";
                    return data;
                }
            }
            catch (Exception ex)
            {
                data.msg = ex.Message;
                return data;
            }
        }
    }
}