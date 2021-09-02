using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    /// <summary>
    /// 密钥对比扩展
    /// </summary>
    public static  class SecretConstantComparer
    {
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static bool IsEqual(string s1, string s2)
        {
            if (s1 == null && s2 == null)
            {
                return true;
            }

            if (s1 == null || s2 == null)
            {
                return false;
            }

            if (s1.Length != s2.Length)
            {
                return false;
            }

            char[] array = s1.ToCharArray();
            char[] array2 = s2.ToCharArray();
            int num = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                num = ((!array[i].Equals(array2[i])) ? (num + 1) : (num + 2));
            }

            return num == s1.Length * 2;
        }
    }
}
