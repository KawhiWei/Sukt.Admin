using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Reflection;
namespace Uwl.Extends.Utility
{
    
    public class PageHelper
    {
    //    //注：使用SqlConnection 需要先添加Nuget包System.Data.Common和System.Data.SqlClient两个程序集

    //    //public DbContextOptions<UwlDbContext> _uwlDbContext;
    //    //public PageHelper(DbContextOptions<UwlDbContext> uwlDbContext)
    //    //{
    //    //    _uwlDbContext = uwlDbContext;
    //    //}
    //    //public static PageDataView<T> GetPageByParam<T>
    //    public static PageDataView<T> GetPageByParam<T>(PageCriteria pageCriteria)
    //    {
    //        if(pageCriteria.ParamsList.Count<=0)
    //             throw new ArgumentException("只少传入一个参数");
    //        using (var context= new SqlConnection(Context.GetCreateConnection()))
    //        {
    //            context.Open();
    //            SqlCommand cmd = context.CreateCommand();
    //            cmd.CommandText = "proc_GetPageDataParamWith2";
    //            Queue<string> queue = new Queue<string>();
    //            queue.Enqueue("param1");
    //            queue.Enqueue("param2");
    //            #region //定义一个指定参数类型的对象
    //            string strparam = String.Empty;
    //            foreach (var item in pageCriteria.ParamsList)
    //            {
    //                SqlParameter parameter = new SqlParameter();
    //                var param = queue.Dequeue();
    //                strparam += $"@{param} {item.ParamType},";//将参数类型传入对象
    //                pageCriteria.Wherecondition = pageCriteria.Wherecondition.Replace(item.ParamName, "@" + param);//将条查询条件改为对应的参数
    //                cmd.Parameters.Add(new SqlParameter(param,SqlDbType.VarChar,100)).Value= $"{item.ParamValue}";
    //            }
    //            #endregion
    //            #region 添加条件
    //            cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar, 100)).Value = pageCriteria.TableName;//查询的表名称
    //            cmd.Parameters.Add(new SqlParameter("@PrimaryKey", SqlDbType.VarChar, 100)).Value = pageCriteria.PrimaryKey;//查询的主键
    //            cmd.Parameters.Add(new SqlParameter("@Fields", SqlDbType.VarChar, 1500)).Value = pageCriteria.Fields;//需要查询出来的字段
    //            cmd.Parameters.Add(new SqlParameter("@Wherecondition", SqlDbType.VarChar, 100)).Value = pageCriteria.Wherecondition;//查询条件
    //            cmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int)).Value = pageCriteria.PageIndex;//当前页码
    //            cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int)).Value = pageCriteria.PageSize;//每页多少条
    //            cmd.Parameters.Add(new SqlParameter("@OrderBySort", SqlDbType.NVarChar, 500)).Value = pageCriteria.OrderBySort;//排序字段
    //            cmd.Parameters.Add(new SqlParameter("@strparam", SqlDbType.NVarChar, 500)).Value = strparam.TrimEnd(',');//查询条件的参数
    //            cmd.Parameters.Add(new SqlParameter("@RecordCount", SqlDbType.Int));//返回多少条
    //            cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;//获取返回的总条数
    //            #endregion
    //            cmd.CommandType = CommandType.StoredProcedure;//执行的语句类型
    //            try
    //            {
    //                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
    //                DataTable dt = new DataTable();
    //                sqlDataAdapter.Fill(dt);
    //                var pagedata = new PageDataView<T>();
    //                pagedata.TotalCount = (int)cmd.Parameters["@RecordCount"].Value;//将拿到的总条数带出来
    //                //pagedata.ItemsList = GetListByModel<T>(dt);
    //                context.Close();
    //                context.Dispose();
    //                cmd.Dispose();
    //                return pagedata;
    //            }
    //            catch (Exception ex)
    //            {
    //                context.Close();
    //                context.Dispose();
    //                cmd.Dispose();
    //                throw new ArgumentException(ex.Message+"执行出现意外，请联系管理员！");
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 使用泛型获取实体类集合
    //    /// </summary>
    //    /// <typeparam name="T">实体类</typeparam>
    //    /// <param name="dt">数据表</param>
    //    /// <returns></returns>
    //    public static IEnumerable<T> GetListByModel<T>(DataTable dt) where T : new()
    //    {
    //        List<T> list = new List<T>();
    //        Type type = typeof(T);
    //        foreach (DataRow item in dt.Rows)
    //        {
    //            T t = new T();
    //            foreach (PropertyInfo p in t.GetType().GetProperties())
    //            {
    //                if (p.CanWrite)
    //                {
    //                    object obj = item[p.Name];
    //                    if (obj != null || obj != DBNull.Value)
    //                    {
    //                        p.SetValue(t, obj);
    //                    }
    //                }
    //                else
    //                {
    //                    continue;
    //                }
    //            }
    //            list.Add(t);
    //        }
    //        return list;
    //    }
    }
}
