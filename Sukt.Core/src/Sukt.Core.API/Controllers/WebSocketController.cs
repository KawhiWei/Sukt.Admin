using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    public class WebSocketController : ControllerBase
    {
        [HttpGet("/login")]
        public async Task Login()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                               HttpContext.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine(HttpContext.Connection.Id);
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            try
            {

            }
            catch (AggregateException )
            {

                throw;
            }
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
