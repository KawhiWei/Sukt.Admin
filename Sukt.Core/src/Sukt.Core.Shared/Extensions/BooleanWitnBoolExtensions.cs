namespace Sukt.Core.Shared.Extensions
{
    public static partial class Extensions
    {

        /// <summary>
        /// 可空Bool转成Bool
        /// </summary>
        /// <param name="bool"></param>
        /// <returns></returns>
        public static bool NullableBoolToBool(bool? @bool)
        {
            return @bool == null || @bool == false ? false : true;
        }

        /// <summary>
        /// 是否等于true
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <returns>如何等于True,返回True,否则False</returns>
        public static bool IsTrue(this object value)
        {
            if (bool.TryParse(value.ToString(), out bool result) && result == true)
            {
                return true;
            }
            return false;
        }
    }
}
