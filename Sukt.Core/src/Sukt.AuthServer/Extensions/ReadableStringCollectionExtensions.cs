using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    /// <summary>
    /// 读取传入字符串扩展
    /// </summary>
    public static class ReadableStringCollectionExtensions
    {
        [DebuggerStepThrough]
        public static NameValueCollection AsNameValueCollection(this IEnumerable<KeyValuePair<string, StringValues>> collection)
        {
            var nv = new NameValueCollection();
            foreach (var field in collection)
            {
                nv.Add(field.Key, field.Value.First());
            }
            return nv;
        }
        [DebuggerStepThrough]
        public static NameValueCollection AsNameValueCollection(this IDictionary<string, StringValues> collection)
        {
            var nv = new NameValueCollection();
            foreach (var field in collection)
            {
                nv.Add(field.Key, field.Value.First());
            }
            return nv;
        }
    }
}
