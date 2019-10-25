using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Uwl.Data.EntityFramework.EFExtensionsSql
{
    public static class EFCoreExtendSql
    {
        private static void CombineParams(ref DbCommand command, Dictionary<string, object> parameterDic)
        {
            if (parameterDic != null)
            {
                foreach (var parameter in parameterDic)
                {

                    command.Parameters.Add(new SqlParameter { ParameterName = parameter.Key.Contains("@") ? parameter.Key : $"@{parameter.Key}", Value = parameter.Value });

                }
            }
        }

        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection dbConn, Dictionary<string, object> parameterDic)
        {
            DbConnection conn = facade.GetDbConnection();
            dbConn = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            DbCommand cmd = conn.CreateCommand();
            if (facade.IsSqlServer())
            {
                cmd.CommandText = sql;
                CombineParams(ref cmd, parameterDic);
            }
            return cmd;

        }



        private static ConcurrentDictionary<Type, PropertyInfo[]> dicQueryTypeGetProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameterDic"></param>
        /// <returns></returns>

        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade facade, string sql, Dictionary<string, object> parameterDic) where T : class, new()
        {
            var ret = new List<T>();
            var type = typeof(T);
            int[] indexes = null;
            var props = dicQueryTypeGetProperties.GetOrAdd(type, k => type.GetProperties());
            ExecuteReader(dr =>
            {
                if (indexes == null)
                {
                    var dic = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
                    for (var a = 0; a < dr.FieldCount; a++)
                        dic.Add(dr.GetName(a), a);
                    indexes = props.Select(a => dic.TryGetValue(a.Name, out var tryint) ? tryint : -1).ToArray();
                }
                ret.Add((T)UtilsExpressionTree.ExecuteArrayRowReadClassOrTuple(type, indexes, dr, 0).Value);
            }, facade, sql, parameterDic);

            return ret.AsEnumerable();
        }

        /// <summary>
        /// 异步sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameterDic"></param>
        /// <returns></returns>

        public static async Task<IEnumerable<T>> SqlQueryAsync<T>(this DatabaseFacade facade, string sql, Dictionary<string, object> parameterDic) where T : class, new()
        {


            var type = typeof(T);
            List<T> ret = new List<T>();
            var props = dicQueryTypeGetProperties.GetOrAdd(type, k => type.GetProperties());
            int[] indexes = null;
            await ExecuteReaderAsync(dr =>
            {
                //ret = ExecuteArrayRowRead<T>(dr);
                if (indexes == null)
                {
                    var dic = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
                    for (var a = 0; a < dr.FieldCount; a++)
                        dic.Add(dr.GetName(a), a);
                    indexes = props.Select(a => dic.TryGetValue(a.Name, out var tryint) ? tryint : -1).ToArray();
                }
                ret.Add((T)UtilsExpressionTree.ExecuteArrayRowReadClassOrTuple(type, indexes, dr, 0).Value);
            }, facade, sql, parameterDic);

            return ret.AsEnumerable();
        }

        private static IEnumerable<T> ExecuteArrayRowRead<T>(DbDataReader dr)
        {
            int[] indexes = null;
            var list = new List<T>();
            var type = typeof(T);
            var props = dicQueryTypeGetProperties.GetOrAdd(type, k => type.GetProperties());

            if (indexes == null)
            {
                var dic = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
                for (var a = 0; a < dr.FieldCount; a++)
                    dic.Add(dr.GetName(a), a);
                indexes = props.Select(a => dic.TryGetValue(a.Name, out var tryint) ? tryint : -1).ToArray();
            }
            list.Add((T)UtilsExpressionTree.ExecuteArrayRowReadClassOrTuple(type, indexes, dr, 0).Value);
            return list;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="readerHander"></param>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameterDic"></param>
        private static void ExecuteReader(Action<DbDataReader> readerHander, DatabaseFacade facade, string sql, Dictionary<string, object> parameterDic)
        {
            using (var command = CreateCommand(facade, sql, out DbConnection conn, parameterDic))
            {
                using (var dr = command.ExecuteReader())
                {
                    while (true)
                    {
                        bool isread = dr.Read();
                        if (isread == false)
                        {
                            break;
                        }
                        readerHander?.Invoke(dr);
                    }
                    dr.Close();
                }

            };

        }


        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="readerHander"></param>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameterDic"></param>
        /// <returns></returns>
        private static async Task ExecuteReaderAsync(Action<DbDataReader> readerHander, DatabaseFacade facade, string sql, Dictionary<string, object> parameterDic)
        {
            using (var command = CreateCommand(facade, sql, out DbConnection conn, parameterDic))
            {
                using (var dr = await command.ExecuteReaderAsync())
                {
                    while (true)
                    {
                        bool isread = await dr.ReadAsync();
                        if (isread == false)
                        {
                            break;
                        }
                        readerHander?.Invoke(dr);
                    }
                    dr.Close();
                }

            };

        }
    }
}
