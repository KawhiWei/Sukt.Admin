//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.Cache.RedisCache;
using Uwl.Data.Model.CacheModel;
using Uwl.Extends.Utility;

namespace Uwl.Common.SignalRMessage
{
    //[EnableCors("SignalRCors")]
    public class SignalRChat: Hub
    {
        //private readonly IRedisCacheManager _redisCacheManager;
        ///// <summary>
        ///// 构造函数注入缓存依赖
        ///// </summary>
        //public SignalRChat(IRedisCacheManager redisCacheManager)
        //{
        //    Console.Out.WriteLine("SignalR构造初始化");
        //    _redisCacheManager=redisCacheManager;
        //}
        /// <summary>
        /// 创建组对象
        /// </summary>
        /// <returns></returns>
        public async override Task OnConnectedAsync()
        {
            //await Console.Out.WriteLineAsync("SignalR链接初始");
            //if (Context.User.Claims.Any())
            //{
            //    await Console.Out.WriteLineAsync("SignalR链接成功");
            //    var Id = Context.User.Claims.FirstOrDefault(x => x.Type == "Id").Value.ToGuid();
            //    var name = Context.User.Claims.FirstOrDefault(x => x.Type == "userName").Value;
            //    var model = new SignalRModel
            //    {
            //        UserId = Id,
            //        SignalRConnectionId = Context.ConnectionId
            //    };
            //    await _redisCacheManager.Set(Id.ToString(), model);//将用户关联的ConnectionID放到缓存中
            //    await Console.Out.WriteLineAsync($"SignalR链接Id放入缓存成功;用户{name}");
            //}
            //else
            //{
            //    await Console.Out.WriteLineAsync("Context.User.Claims未获取到用户信息！");
            //}
            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 释放组对象
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Console.Out.WriteLineAsync("SignalR链接断开了！");
            if (Context.User.Claims.Any())
            {
                var Id = Context.User.Claims.FirstOrDefault(x => x.Type == "Id").Value.ToGuid();//链接释放将用户关联的ConnectionID从缓存中移除
                //await _redisCacheManager.Remove(Id.ToString());
            }
            await base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 发送消息给指定用户
        /// </summary>
        /// <param name="SenderId">发送人Id</param>
        /// <param name="SenderName">发送人名称</param>
        /// <param name="ReceiverId">接收人ID</param>
        /// <param name="Message">发送消息</param>
        /// <returns></returns>
        public async Task SendMessage(string SenderId, string SenderName, string ReceiverId, string Message)
        {
            //var SignalRModel = await _redisCacheManager.Get<SignalRModel>(ReceiverId.ToString());
            //if (SignalRModel != null)
            //{
            //    await Clients.Client(SignalRModel.SignalRConnectionId).SendAsync("ReceiveMessage", SenderName, Message);
            //}
            //else
            //{
                await Clients.All.SendAsync("ReceiveMessage", SenderName, Message);
            //}
        }
        /// <summary>
        /// 服务端主动向客户端发送数据
        /// 定于一个通讯管道，用来管理我们和客户端的连接
        ///  1、客户端调用 GetLatestCount，就像订阅
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public async Task GetLatestCount(string random)
        {
            //2、服务端主动向客户端发送数据，名字千万不能错
            await Clients.All.SendAsync("ReceiveUpdate", "12125454512154");
        }
    }
}
