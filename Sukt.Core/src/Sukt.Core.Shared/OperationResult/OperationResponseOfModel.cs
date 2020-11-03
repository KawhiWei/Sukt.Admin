using Sukt.Core.Shared.Enums;

namespace Sukt.Core.Shared.OperationResult
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class OperationResponse<TData> : ResultBase
    {
        public OperationResponse() : this(OperationEnumType.Success)
        {
        }

        public OperationResponse(OperationEnumType type = OperationEnumType.Success) : this("", default(TData), type)
        {
        }

        public OperationResponse(string message, OperationEnumType type) : this(message, default(TData), type)
        {
        }

        public OperationResponse(string message, TData data, OperationEnumType type)
        {
            Message = message;
            Type = type;
            Data = data;
        }

        public TData Data { get; set; }

        public OperationEnumType Type { get; set; }

        public override bool Success => Type == OperationEnumType.Success;

        public bool Error()
        {
            return Type != OperationEnumType.Success;
        }

        public bool Exp()
        {
            return Type == OperationEnumType.Exp;
        }
    }
}