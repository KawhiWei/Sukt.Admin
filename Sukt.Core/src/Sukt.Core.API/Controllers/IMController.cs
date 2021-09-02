using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.WebSocketServer;
using System;
using System.Collections.Generic;
using Sukt.WebSocketServer.Attributes;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IMController : ControllerBase, IWebSocketSession
    {

        public HttpContext WebSocketHttpContext { get ; set ; }
        public WebSocket WebSocketClient { get ; set ; }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebSocket]
        [HttpPost]
        public async Task<string> Login(string uid)
        {
            await Task.CompletedTask;
            //登录
            //把ContextId与uid关联
            
            Console.WriteLine($"12132as1d32sa1d3as1d32{uid}---------->{WebSocketHttpContext.Connection.Id}");
            return WebSocketHttpContext.Connection.Id;
        }
    }
}
