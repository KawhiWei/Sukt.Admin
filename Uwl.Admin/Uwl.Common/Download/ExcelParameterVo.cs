using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Uwl.Common.Download
{
    /// <summary>
    /// Excel列头帮助类
    /// </summary>
    public class ExcelParameterVo
    {
        public string ColumnName
        {
            get;
            set;
        }

        public int ColumnWidth
        {
            get;
            set;
        }

        public int Sort
        {
            get;
            set;
        }

        public PropertyInfo Property
        {
            get;
            set;
        }
    }
}
