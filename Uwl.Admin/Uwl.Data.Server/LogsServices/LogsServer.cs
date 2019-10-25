using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Server.LambdaTree;
using Uwl.Domain.LogsInterface;
using Uwl.Extends.Utility;

namespace Uwl.Data.Server.LogsServices
{
    /// <summary>
    /// 日志业务层实现
    /// </summary>
    public class LogsServer : ILogsServer
    {
        private ILogRepositoty _logRepositoty;
        /// <summary>
        /// 通过构造函数注入Logs领域层
        /// </summary>
        /// <param name="logRepositoty"></param>
        public LogsServer(ILogRepositoty logRepositoty)
        {
            _logRepositoty = logRepositoty;
        }
        public void Insert(Logs logs)
        {
            _logRepositoty.Insert(logs);
        }
        public IEnumerable<Logs> GetLogsByPage(LogsQueryModel logsQuery,out int Total)
        {
            var query= ExpressionBuilder.True<Logs>();
            if(!logsQuery.Title.IsNullOrEmpty())
            {
                query = query.And(x => x.Title.Contains(logsQuery.Title));
            }
            Total = _logRepositoty.Count(query);
            return _logRepositoty.PageBy(logsQuery.PageIndex, logsQuery.PageSize, query);
        }
    }
}
