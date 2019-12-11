using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.LogsServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UwlAPI.Tools.Controllers
{
    
    /// <summary>
    /// 日志管理
    /// </summary>
    [Route("api/GetLogs")]
    //[EnableCors("AllRequests")]
    //[AllowAnonymous]//允许匿名访问
    public class LogsController : BaseController<LogsController>
    {
        private ILogsServer _logsServer=null;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="logsServer"></param>
        /// <param name="logger">日志记录</param>
        public LogsController(ILogsServer logsServer,ILogger<LogsController> logger):base(logger)
        {
            _logsServer = logsServer;
        }
        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <returns></returns>
        [Route("logs")]
        [HttpGet]
        public ActionResult GetLogsListByPage(LogsQueryModel logsQuery)
        {
            var list = _logsServer.GetLogsByPage(logsQuery, out int Total);
            return Json(new MessageModel<PageModel<Logs>>(){
                success = true,msg = "数据获取成功",
                response = new PageModel<Logs>()
                {
                    TotalCount = Total,
                    data = list.ToList(),
                }
            });
        }        
    }
}
