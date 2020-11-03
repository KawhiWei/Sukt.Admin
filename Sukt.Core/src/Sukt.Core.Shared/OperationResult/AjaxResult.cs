using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions;

namespace Sukt.Core.Shared.OperationResult
{
    public class AjaxResult
    {
        public AjaxResult() : this(null)
        {
        }

        public AjaxResult(AjaxResultType type = AjaxResultType.Success) : this("", null, type)
        {
        }

        public AjaxResult(string message, AjaxResultType type = AjaxResultType.Success, object data = null) : this(message, data, type)
        {
        }

        public AjaxResult(string message, object data, AjaxResultType type)
        {
            this.Message = message;
            this.Data = data;
            this.Type = type;
        }

        public AjaxResult(string message, bool success, object data, AjaxResultType type)
        {
            this.Message = message;
            this.Data = data;
            this.Type = type;
            this.Success = success;
        }

        /// <summary>
        /// 消息
        /// </summary>

        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>

        public object Data { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>

        public bool Success { get; set; }

        /// <summary>
        /// 返回类型
        /// </summary>

        public AjaxResultType Type { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeeded()
        {
            return Type == AjaxResultType.Success;
        }

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool Error()
        {
            return Type == AjaxResultType.Error;
        }

        /// <summary>
        /// 转成对象
        /// </summary>
        /// <returns></returns>
        public object ToObject()
        {
            return new { Data, Message, Success, Type };
        }

        /// <summary>
        /// 转成JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return this.ToObject().ToJson();
        }
    }
}