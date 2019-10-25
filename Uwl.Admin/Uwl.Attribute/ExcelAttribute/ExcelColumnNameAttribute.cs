using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Attribute.ExcelAttribute
{
    public class ExcelColumnNameAttribute : System.Attribute
    {
        private int _columnWith = 20;
        public string ColumnName
        {
            get;
        }
        public int ColumnWith
        {
            get
            {
                return this._columnWith;
            }
            set
            {
                this._columnWith = value;
            }
        }
        public int Sort
        {
            get;
            set;
        }
        public ExcelColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
