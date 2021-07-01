using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointRouterHandler
{
    /// <summary>
    /// 断点路由配置类
    /// </summary>
    public class Endpoint
    {
        public Endpoint(string name, PathString path, Type handler)
        {
            Name = name;
            Path = path;
            Handler = handler;
        }

        /// <summary>
        /// 处理器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public PathString Path { get; set; }
        /// <summary>
        /// 路由处理器
        /// </summary>
        public Type Handler { get; set; }
    }
}
