using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.SignalrHubs
{
    /// <summary>
    /// 实时消息推送
    /// </summary>
    public class SignalRChatHub : Hub
    {
        /// <summary>
        /// 创建组对象
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 释放组对象
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            var ss = InvokeHubMethod();
            var s = Context.User.Identity.Name;
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        /// <summary>
        /// 服务端主动向客户端发送数据
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        //定于一个通讯管道，用来管理我们和客户端的连接
        //1、客户端调用 GetLatestCount，就像订阅
        public async Task GetLatestCount(string random)
        {
            //2、服务端主动向客户端发送数据，名字千万不能错
            await Clients.All.SendAsync("ReceiveUpdate", "12125454512154");
        }
        /// <summary>
        /// 获取用户连接ID
        /// </summary>
        /// <returns></returns>
        public string InvokeHubMethod()
        {
            return Context.ConnectionId;
        }
    }
}
