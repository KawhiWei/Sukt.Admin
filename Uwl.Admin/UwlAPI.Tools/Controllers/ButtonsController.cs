using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Model.VO.ButtonVO;
using Uwl.Data.Server.ButtonServices;
using Uwl.Extends.Utility;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 按钮API接口
    /// </summary>
    [Route("api/Button")]
    [ApiController]
    public class ButtonsController : BaseController
    {
        private readonly IButtonServer _buttonServer;
        /// <summary>
        /// 注入服务层
        /// </summary>
        /// <param name="buttonServer">按钮服务层</param>
        public ButtonsController(IButtonServer buttonServer)
        {
            _buttonServer = buttonServer;
        }
        /// <summary>
        /// 分页获取按钮
        /// </summary>
        /// <param name="buttonQuery"></param>
        /// <returns></returns>
        [Route("PageButton")]
        // GET: api/Buttons/5
        [HttpGet]
        public MessageModel<PageModel<ButtonViewMoel>> GetButtonByPage([FromQuery]ButtonQuery buttonQuery)
        {
            var role = RoleIds;
            var list = _buttonServer.GetQueryByPage(buttonQuery);
            return new MessageModel<PageModel<ButtonViewMoel>>()
            {
                success = true,
                msg = "数据获取成功",
                response = new PageModel<ButtonViewMoel>()
                {
                    TotalCount = list.Item2,
                    data = list.Item1,
                }
            };
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sysButton"></param>
        /// <returns></returns>
        // POST: api/Buttons
        [Route("AddButton")]
        [HttpPost]
        public async Task<MessageModel<string>> AddButton([FromBody] SysButton sysButton)
        {
            var data = new MessageModel<string>();
            data.success = await _buttonServer.AddButton(sysButton);
            if (data.success)
            {
                data.msg = "按钮添加成功";
            }
            return data;
        }
        /// <summary>
        /// 修改按钮信息
        /// </summary>
        /// <param name="sysButton"></param>
        /// <returns></returns>
        [Route("UpdateButton")]
        // PUT: api/Buttons/5
        [HttpPut]
        public async Task<MessageModel<string>> UpdateButton([FromBody]SysButton sysButton)
        {
            var data = new MessageModel<string>();
            data.success = await _buttonServer.UpdateButton(sysButton);
            if (data.success)
            {
                data.msg = "按钮修改成功";
            }
            return data;
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Route("DeleteButton")]
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(string Ids)
        {
            var list = JsonConvert.DeserializeObject<List<Guid>>(Ids);
            var data = new MessageModel<string>();
            try
            {
                data.success = await _buttonServer.DeleteButton(list);
                if (data.success)
                {
                    data.msg = "删除成功";
                }
                return data;
            }
            catch (Exception ex)
            {

                data.success = false;
                data.msg = "删除失败!" + ex.Message;
                return data;
            }
        }
    }
}
