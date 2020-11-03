using Sukt.Core.Shared.Enums;

namespace Sukt.Core.Shared.OperationResult
{
    public class OperationResponse : OperationResponse<object>
    {
        public OperationResponse() : base(OperationEnumType.Success)
        {
        }

        public OperationResponse(OperationEnumType type = OperationEnumType.Success) : base("", null, type)
        {
        }

        public OperationResponse(string message, OperationEnumType type) : base(message, null, type)
        {
        }

        public OperationResponse(string message, object data, OperationEnumType type) : base(message, data, type)
        {
        }

        /// <summary>
        /// 成功
        /// </summary>
        public static OperationResponse Ok()
        {
            return Ok(string.Empty, null);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">提示消息</param>
        public static OperationResponse Ok(string message)
        {
            return Ok(message, null);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回成功数据</param>
        /// <returns></returns>
        public static OperationResponse Ok(object data)
        {
            return Ok(string.Empty, data);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="data">返回成功数据</param>
        /// <returns></returns>
        public static OperationResponse Ok(string message, object data)
        {
            return new OperationResponse(message, data, OperationEnumType.Success);
        }

        public static OperationResponse Error(string message)
        {
            return Error(message, null);
        }

        public static OperationResponse Error(object data)
        {
            return Error(string.Empty, data);
        }

        public static OperationResponse Error(string message, object data)
        {
            return new OperationResponse(message, data, OperationEnumType.Error);
        }
    }
}