using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetSuktAuthServerBaseUrl(this HttpContext context)
        {
            return context.GetSuktAuthServerHost();

        }
        public static string GetSuktAuthServerHost(this HttpContext context)
        {
            var request = context.Request;
            return $"{ request.Scheme }://{request.Host.ToUriComponent()}";
        }
        //public static string GetSuktAuthServerBasePath(this HttpContext context)
        //{
        //    return context.
        //}
    }
}
