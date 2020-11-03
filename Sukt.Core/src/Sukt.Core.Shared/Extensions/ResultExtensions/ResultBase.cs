namespace Sukt.Core.Shared.Extensions.ResultExtensions
{
    /// <summary>
    /// 返回前端数据基类模型
    /// </summary>
    public class ResultBase : IResultBase
    {
        public virtual bool Success { get; set; }

        public virtual string Message { get; set; }
    }

    public interface IResultBase
    {
        bool Success { get; }

        string Message { get; }
    }
}