using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.Assist
{
    public class ScheduleQuery: BaseQuery
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 触发器类型
        /// </summary>
        public int TriggerType { get; set; } = -1;
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool? IsStart { get; set; }
    }
}
