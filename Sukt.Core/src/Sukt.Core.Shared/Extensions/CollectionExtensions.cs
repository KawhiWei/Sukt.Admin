using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sukt.Core.Shared.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 根据Index下标移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<T> Remove<T>(this List<T> list, int index) where T : class, new()
        {
            list.RemoveAt(index);
            return list;

        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> RemoveAll<T>(this List<T> list) where T : class, new()
        {

            list.NotNullOrEmpty(nameof(list));
            for (int i = list.Count - 1; i >= 0; i--)
            {
                list.RemoveAt(i);
            }
            return list;
        }
        /// <summary>串联对象数组的各个元素，其中在每个元素之间使用指定的分隔符。</summary>
        /// <returns>一个由 <paramref name="values" /> 的元素组成的字符串，这些元素以 <paramref name="separator" /> 字符串分隔。如果 <paramref name="values" /> 为空数组，该方法将返回 <see cref="F:System.String.Empty" />。</returns>
        /// <param name="separator">要用作分隔符的字符串。只有在 <paramref name="separator" /> 具有多个元素时，<paramref name="values" /> 才包括在返回的字符串中。</param>
        /// <param name="values">一个集合，其中包含要连接的元素。</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="values" /> 为 null。</exception>
        public static string ToJoin<TSource>(this IEnumerable<TSource> values, string separator = ",") where TSource : IEnumerable
        {
            values = values.Where(o => !o.AsTo<string>().IsNullOrEmpty());
            return string.Join(separator, values);
        }
        /// <summary>
        /// 去重
        /// </summary>
        /// <typeparam name="TSource">去重数据源</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keySelector">键条件</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(gropby => gropby.First());
        }
        /// <summary>
        /// 去重
        /// </summary>
        /// <typeparam name="TSource">去重数据源</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keySelector">键条件</param>
        /// <returns>返回去重后集合数据</returns>
        public static IList<TSource> ToDistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector)
        {

            return source.DistinctBy(keySelector).ToList();
        }
        /// <summary>
        /// 把集合转成SqlIn
        /// </summary>
        /// <typeparam name="TSource">源</typeparam>
        /// <param name="values">要转换的值</param>
        /// <param name="separator">分割符</param>
        /// <param name="left">左边符</param>
        /// <param name="right">右边符</param>
        /// <returns>返回组装好的值，例如"'a','b'"</returns>
        public static string ToSqlIn<TSource>(this IEnumerable<TSource> values, string separator = ",", string left = "'", string right = "'")
        {
            StringBuilder sb = new StringBuilder();
            var enumerable = values as TSource[] ?? values.ToArray();
            if (!enumerable.Any())
            {
                return string.Empty;
            }

            enumerable.ToList().ForEach(o =>
            {

                sb.AppendFormat("{0}{1}{2}{3}", left, o, right, separator);

            });
            string newStr = sb.ToString()?.TrimEnd($"{separator}".ToCharArray());
            return newStr;
        }
        /// <summary>
        /// 根据集合字典转成字典
        /// </summary>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="keyValuePairs">数据源</param>
        /// <returns>返回所需的字典</returns>
        public static IDictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            keyValuePairs.NotNullOrEmpty(nameof(keyValuePairs));
            IDictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> keys in keyValuePairs)
            {
                dic.Add(keys.Key, keys.Value);
            }
            return dic;
        }
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, bool condition) where TSource : IEnumerable
        {
            source.NotNullOrEmpty(nameof(source));
            predicate.NotNull(nameof(predicate));
            return condition ? source.Where(predicate) : source;
        }
        /// <summary>
        /// 给IEnumerable拓展ForEach方法
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="iEnumberable">数据源</param>
        /// <param name="func">方法</param>
        public static void ForEach<T>(this IEnumerable<T> iEnumberable, Action<T> func)
        {
            foreach (var item in iEnumberable)
            {
                func(item);
            }
        }
        /// <summary>
        /// 给IEnumerable拓展ForEach方法
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="iEnumberable">数据源</param>
        /// <param name="func">方法</param>
        public static void ForEach<T>(this IEnumerable<T> iEnumberable, Action<T, int> func)
        {

            var array = iEnumberable.ToArray();
            for (int i = 0; i < array.Count(); i++)
            {
                func(array[i], i);
            }
        }
        /// <summary>
        /// 将列表转换为树形结构（泛型无限递归）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据</param>
        /// <param name="rootwhere">根条件</param>
        /// <param name="childswhere">节点条件</param>
        /// <param name="addchilds">添加子节点</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<T> ToTree<T>(this List<T> list, Func<T, T, bool> rootwhere, Func<T, T, bool> childswhere, Action<T, IEnumerable<T>> addchilds, T entity = default(T))
        {
            var treelist = new List<T>();
            //空树
            if (list == null || list.Count == 0)
            {
                return treelist;
            }
            if (!list.Any<T>(e => rootwhere(entity, e)))
            {
                return treelist;
            }
            //树根
            if (list.Any<T>(e => rootwhere(entity, e)))
            {
                treelist.AddRange(list.Where(e => rootwhere(entity, e)));
            }
            //树叶
            foreach (var item in treelist)
            {
                if (list.Any(e => childswhere(item, e)))
                {
                    var nodedata = list.Where(e => childswhere(item, e)).ToList();
                    foreach (var child in nodedata)
                    {
                        //添加子集
                        var data = list.ToTree(childswhere, childswhere, addchilds, child);
                        addchilds(child, data);
                    }
                    addchilds(item, nodedata);
                }
            }
            return treelist;
        }
        /// <summary>
        /// 把集合的元素转成指定的类型
        /// </summary>
        /// <typeparam name="TTarget">要转换的类型</typeparam>
        /// <param name="source">转换的数据源</param>
        /// <returns>返回转换后的集合</returns>
        public static IEnumerable<TTarget> AsToAll<TTarget>(this IEnumerable source)
        {
            source.NotNull(nameof(source));
            IEnumerable<TTarget> enumerable = source as IEnumerable<TTarget>;
            if (enumerable != null)
            {
                return enumerable;
            }
            return CastIterator<TTarget>(source);
        }
        private static IEnumerable<TResult> CastIterator<TResult>(IEnumerable source)
        {
            foreach (object current in source)
            {
                yield return (current.AsTo<TResult>());
            }
            yield break;
        }
    }
}
