using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Uwl.Extends.Utility
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 多行转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DatatableToObjectList<T>(this DataTable dt)where T:new()
        {
            if (dt == null || dt.Rows.Count <= 0)
                return new List<T>();
            dynamic list = new List<T>();
            foreach (dynamic row in dt.Rows)
            {
                list.Add(DataRowToObject<T>(row));
            }
            return list;
        }
        /// <summary>
        /// 单行数据转实体
        /// </summary>
        /// <typeparam name="T">可实例化的实体</typeparam>
        /// <param name="dr">数据源</param>
        /// <returns>实体数据</returns>
        public static T DataRowToObject<T>(DataRow dr) where T : new()
        {
            if (dr == null || dr.Table.Columns.Count <= 0)
                return new T();
            dynamic t = new T();
            dynamic propers = t.GetType().GetProperties();
            try
            {
                foreach (var pro in propers)
                {
                    var cols = dr.Table.Columns;
                    foreach (dynamic c in cols)
                    {
                        if (!string.Equals(pro.Name, c.ColumnName, StringComparison.CurrentCultureIgnoreCase)) continue;
                        pro.SetValue(t, ChangeType(dr[c.ColumnName], pro.PropertyType), null);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                //ex.Message;
                return new T();
            }
            return t;
        }
        private static object ChangeType(object value, Type conversion)
        {
            var t = conversion;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value == DBNull.Value)
                {
                    return null;
                }
                t = Nullable.GetUnderlyingType(t);
            }
            else if (value == null || value == DBNull.Value)
            {
                return null;
            }
            if (t != null && t.IsEnum)
            {
                var eunms = t.GetEnumValues();
                foreach (var ar in eunms)
                {
                    if (Convert.ToInt32(value) == (int)ar)
                    {
                        return Convert.ChangeType(ar, t);
                    }
                }
            }
            return Convert.ChangeType(value, t);
        }
    }
}
