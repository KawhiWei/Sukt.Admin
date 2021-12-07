using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.WebSocketServer;
using System;
using System.Collections.Generic;
using Sukt.WebSocketServer.Attributes;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Sukt.WebSocketServer.MvcHandler;
using System.Text;
using Sukt.Module.Core.Extensions;
using System.Threading;

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
        /// <returns></returns>
        [WebSocket]
        [HttpPost]
        public async Task<string> Login(string uid)
        {
            await Task.CompletedTask;
            //登录
            //把ContextId与uid关联
            
            Console.WriteLine($"12132as1d32sa1d3as1d32{uid}---------->{WebSocketHttpContext.Connection.Id}");
            Console.WriteLine( MvcChannelHandler.Clients);


            var msg = new { imsgd=  "asdasdasdasdasdadsa啊实打实打算打赏阿斯顿阿斯顿阿斯顿撒旦阿萨阿萨da"};

            var replyMess = Encoding.UTF8.GetBytes(msg.ToJson());
            //await WebSocketClient.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, CancellationToken.None);
            return "ad143as1d3asd3as1d3as23d32aas";
        }
    }
}
