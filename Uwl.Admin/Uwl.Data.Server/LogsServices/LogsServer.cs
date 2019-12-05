using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uwl.Common;
using Uwl.Common.LambdaTree;
using Uwl.Data.Model.BaseModel;
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
        public void Insert(string Title, string Contents, string Ip, EnumTypes types = EnumTypes.其他分类,string TypeName="",string Others="", string oldXML = "", string newXML = "")
        {
            Logs logs = new Logs();
            logs.Id = Guid.NewGuid();
            logs.CreatedDate = DateTime.Now;
            logs.CreatedId = Guid.NewGuid();
            logs.CreatedName = "admin";
            logs.UpdateId = Guid.NewGuid();
            logs.UpdateDate = DateTime.Now;
            logs.UpdateName = "admin";
            logs.Title = Title;
            logs.TypeName = TypeName;
            logs.IPAddress = Ip;
            logs.Contents = Contents;
            logs.Others = Others;
            logs.OldXml = string.IsNullOrEmpty(oldXML) ? null : oldXML;
            logs.NewXml = string.IsNullOrEmpty(newXML) ? null : newXML;
            _logRepositoty.Insert(logs);
        }
        public List<Logs> GetLogsByPage(LogsQueryModel logsQuery,out int Total)
        {
            var query= ExpressionBuilder.True<Logs>();
            if(!logsQuery.Title.IsNullOrEmpty())
            {
                query = query.And(x => x.Title.Contains(logsQuery.Title));
            }
            Total = _logRepositoty.Count(query);
            return _logRepositoty.PageBy(logsQuery.PageIndex, logsQuery.PageSize, query).ToList();
        }
    }
}
