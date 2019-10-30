using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Common.LogsMethod
{
    public class Log
    {
        //private ILogsServer _logsServer;
        ////创建一个委托
        //private delegate void dgWriteLog(Logs logs);
        //public Log(ILogsServer logsServer)
        //{
        //    _logsServer = logsServer;
        //}
        ///// <summary>
        ///// 具体添加日志方法
        ///// </summary>
        ///// <param name="logs"></param>
        //public void addlog(Logs logs)
        //{
        //    _logsServer.Insert(logs);
        //}
        ///// <summary>
        ///// 定义一个静态方法里面通拓委托来实现调用添加日志方法
        ///// </summary>
        ///// <param name="logs"></param>
        //public void Add(string Title, string Contents, string Ip, EnumTypes types = EnumTypes.其他分类, string oldXML = "", string newXML = "")
        //{
        //    Logs logs = new Logs();
        //    logs.Id = Guid.NewGuid();
        //    logs.CreatedDate = DateTime.Now;
        //    logs.CreatedId = Guid.NewGuid();
        //    logs.CreatedName = "admin";
        //    logs.UpdateId = Guid.NewGuid();
        //    logs.UpdateDate = DateTime.Now;
        //    logs.UpdateName = "admin";
        //    logs.Title = Title;
        //    logs.TypeName = "11";
        //    logs.IPAddress = Ip;
        //    logs.Contents = Contents;
        //    logs.Others = "11";
        //    logs.OldXml = string.IsNullOrEmpty(oldXML) ? null : oldXML;
        //    logs.NewXml = string.IsNullOrEmpty(newXML) ? null : newXML;
        //    dgWriteLog writeLog = new dgWriteLog(addlog);
        //    writeLog.Invoke(logs);
        //}
    }


}
