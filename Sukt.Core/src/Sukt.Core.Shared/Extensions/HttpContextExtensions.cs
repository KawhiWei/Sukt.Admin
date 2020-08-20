using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sukt.Core.Shared.Extensions
{
    public static partial class Extensions
    {

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static List<string> GetHostIP()
        {
            List<string> Ip = new List<string>();
            IPAddress[] p = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (var i in p)
            {
                if (i.IsIPv6LinkLocal && i.IsIPv6Teredo)//如果是非IPv6地址,添加到list
                {
                    Ip.Add(i.ToString());//
                }
            }
            return Ip;
        }

        /// <summary>
        /// 获取请求端的Ip地址
        /// string ip1 = HttpContext.Request.Headers["X-Real-IP"]; 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIP(this HttpContext context)
        {
            //HttpContext.
            var ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
