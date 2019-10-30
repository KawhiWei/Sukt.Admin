using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    //日志实体类
    public class Logs: Entity
    {
        /// <summary>
        /// 修改标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 修改的类型
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 客户机IP
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 修改连接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 修改内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string Others { get; set; }
        /// <summary>
        /// 修改前数据
        /// </summary>
        public string OldXml { get; set; }
        /// <summary>
        /// 修改后数据
        /// </summary>
        public string NewXml { get; set; }
        /// <summary>
        /// 测试字段
        /// </summary>
        public string Test { get; set; }
    }
}
