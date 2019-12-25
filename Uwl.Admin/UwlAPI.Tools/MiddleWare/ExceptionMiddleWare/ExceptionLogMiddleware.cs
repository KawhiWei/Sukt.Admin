using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Uwl.Common.LogsMethod;
using Uwl.Data.Model.Result;

namespace UwlAPI.Tools.MiddleWare.ExceptionMiddleWare
{
    /// <summary>
    /// 全局异常处理中间件
    /// </summary>
    public class ExceptionLogMiddleware
    {
        //private readonly ILogHelper _logHelper;
        ///// <summary>
        ///// 注入日志记录
        ///// </summary>
        ///// <param name="logHelper"></param>
        //public ExceptionLogMiddleware(ILogHelper logHelper)
        //{
        //    _logHelper = logHelper;
        //}
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ExceptionLogMiddleware(RequestDelegate  next)
        {
            _next = next;
        }
        /// <summary>
        /// 异常中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext  context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogServer.WriteErrorLog(context.Request.Path.Value.Replace("/",""), $"{context.Request.Path.Value}接口请求错误", ex);
                await HandleExceptionAsync(context, ex);
            }
            
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }
        /// <summary>
        /// 写入日志并且返回自定义消息处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            //记录日志
            //var stg =exception.GetBaseException().ToString();
            //返回友好的提示
            var response = context.Response;
            //状态码
            if (exception is UnauthorizedAccessException)
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (exception is Exception)
                response.StatusCode = (int)HttpStatusCode.BadRequest;

            response.ContentType = context.Request.Headers["Accept"];

            if (response.ContentType.ToLower() == "application/xml")
            {
                await response.WriteAsync(Object2XmlString("服务器异常,请稍后再试!")).ConfigureAwait(false);
            }
            else
            {
                response.ContentType = "application/json";

                var model= new MessageModel<string>(){
                    msg= "服务器异常,请稍后再试!",
                };
                await response.WriteAsync(JsonConvert.SerializeObject(model)).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 对象转为Xml
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string Object2XmlString(object o)
        {
            StringWriter sw = new StringWriter();
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(sw, o);
            }
            catch
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Dispose();
            }
            return sw.ToString();
        }
    }
}
