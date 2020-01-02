using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.Assist
{
    public class BaseQuery
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        //每页条数
        public int PageSize { get; set; } = 15;
    }
}
