using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    //日志实体类
    public class Logs: Entity
    {
        ////主键ID
        //public Guid Id { get; set; }
        //修改标题
        public string Title { get; set; }
        //修改的类型
        public string TypeName { get; set; }
        //客户机IP
        public string IPAddress { get; set; }
        //修改连接
        public string Url { get; set; }
        //修改内容
        public string Contents { get; set; }
        //浏览器版本
        public string Others { get; set; }
        //修改之前数据
        public string OldXml { get; set; }
        //修改之后数据
        public string NewXml { get; set; }
        //测试字段
        public string Test { get; set; }
    }
}
