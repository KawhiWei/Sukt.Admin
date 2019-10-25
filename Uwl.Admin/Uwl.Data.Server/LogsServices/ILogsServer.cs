using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.Server.LogsServices
{
    /// <summary>
    /// 日志业务层接口
    /// </summary>
    public interface ILogsServer
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="logs"></param>
        void Insert(Logs logs);
        /// <summary>
        /// 分页查询操作日志
        /// </summary>
        /// <param name="logsQuery"></param>
        /// <returns></returns>
        IEnumerable<Logs> GetLogsByPage(LogsQueryModel logsQuery, out int Total);
    }
}
