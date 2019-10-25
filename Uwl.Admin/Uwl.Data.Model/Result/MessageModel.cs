using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.Result
{
    /// <summary>
    /// 获取数据返回类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageModel<T>
    {
        public bool success { get; set; } = false;
        public string msg { get; set; } = "获取数据异常";
        public T response { get; set; } 
    }
}
