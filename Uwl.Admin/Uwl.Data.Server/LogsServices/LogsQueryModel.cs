using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.Assist;

namespace Uwl.Data.Server.LogsServices
{
    public class LogsQueryModel:BaseQuery
    {
        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }
}
