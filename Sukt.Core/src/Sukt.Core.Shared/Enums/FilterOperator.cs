using Sukt.Core.Shared.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Enums
{
    [Description("过滤操作器")]
    public enum FilterOperator
    {
        /// <summary>
        /// 
        /// </summary>
        [FilterCode("==")]
        [Description("等于")]
        Equal,

        [FilterCode(">")]
        [Description("大于")]
        GreaterThan,

        [FilterCode(">=")]
        [Description("大于或等于")]
        GreaterThanOrEqual,

        [FilterCode("<")]
        [Description("小于")]
        LessThan,

        [FilterCode("<=")]
        [Description("小于或等于")]
        LessThanOrEqual,

        [FilterCode("!=")]
        [Description("不等于")]
        NotEqual,

        [FilterCode("Contains")]
        [Description("包含")]
        In,

        [FilterCode("Contains")]
        [Description("模糊查询")]
        Like,
    }
    [Description("过滤连接器")]
    public enum FilterConnect
    {
        [FilterCode("and")]
        And,

        [FilterCode("or")]
        Or
    }
}
