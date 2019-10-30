using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Common;
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
        void Insert(string Title, string Contents, string Ip, EnumTypes types = EnumTypes.其他分类, string TypeName = "", string Others = "", string oldXML = "", string newXML = "");
        /// <summary>
        /// 分页查询操作日志
        /// </summary>
        /// <param name="logsQuery"></param>
        /// <returns></returns>
        List<Logs> GetLogsByPage(LogsQueryModel logsQuery, out int Total);
    }
}
