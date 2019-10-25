using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Uwl.Attribute.ExcelAttribute;

namespace Uwl.Extends.Utility
{
    public static class EppLusExtensions
    {
        public static int GetColumnByName(this string columnName, ExcelWorksheet ws)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            return ws.Cells["1:1"].First(c => c.Value.ToString() == columnName).Start.Column;
        }
        /// <summary>
        /// 读取Excel sheet扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excelWorksheet"></param>
        /// <returns></returns>
        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet excelWorksheet) where T : new()
        {
            Func<CustomAttributeData, bool> columnOnly = y => y.AttributeType == typeof(ExcelReadColumnNameAttribute);
            //通过反射获取对象的读取Excel列名特性
            var columns = typeof(T).GetProperties()
                .Where(x => x.CustomAttributes.Any(columnOnly))
                .Select(col => new
                {
                    Property = col,
                    Column = col.GetCustomAttributes<ExcelReadColumnNameAttribute>().First().ColumnName
                }).ToList();
            var rows = excelWorksheet.Cells
                .Select(cell => cell.Start.Row)
                .Distinct()
                .OrderBy(x => x);
            var collection = rows.Skip(1)
                .Select(row =>
                {
                    var tnew = new T();
                    columns.ForEach(col =>
                    {
                        var val = excelWorksheet.Cells[row, col.Column.GetColumnByName(excelWorksheet)];
                        //var val = excelWorksheet.Cells[row, GetColumnByName(excelWorksheet, col.Column)];
                        if (val.Value == null)
                        {
                            col.Property.SetValue(tnew, null);
                            return;
                        }
                        // 如果Person类的对应字段是int的，该怎么怎么做……
                        if (col.Property.PropertyType == typeof(int))
                        {
                            col.Property.SetValue(tnew, val.GetValue<int>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(int?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<int?>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(long))
                        {
                            col.Property.SetValue(tnew, val.GetValue<long>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(long?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<long?>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(decimal))
                        {
                            col.Property.SetValue(tnew, val.GetValue<decimal>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(decimal?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<decimal?>());
                            return;
                        }
                        // 如果Person类的对应字段是double的，该怎么怎么做……
                        if (col.Property.PropertyType == typeof(double))
                        {
                            col.Property.SetValue(tnew, val.GetValue<double>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(double?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<double?>());
                            return;
                        }
                        // 如果Person类的对应字段是DateTime?的，该怎么怎么做……
                        if (col.Property.PropertyType == typeof(DateTime?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<DateTime?>());
                            return;
                        }
                        // 如果Person类的对应字段是DateTime的，该怎么怎么做……
                        if (col.Property.PropertyType == typeof(DateTime))
                        {
                            col.Property.SetValue(tnew, val.GetValue<DateTime>());
                            return;
                        }
                        // 如果Person类的对应字段是bool的，该怎么怎么做……
                        if (col.Property.PropertyType == typeof(bool?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<bool>());
                            return;
                        }
                        if (col.Property.PropertyType == typeof(bool?))
                        {
                            col.Property.SetValue(tnew, val.GetValue<bool>());
                            return;
                        }
                        col.Property.SetValue(tnew, val.GetValue<string>());
                    });

                    return tnew;
                });
            return collection;
        }
    }
}
