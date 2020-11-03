namespace Sukt.Core.Shared.Entity
{
    public interface IOutputDto
    {
    }

    /// <summary>
    /// 继承第一层DTO接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IOutputDto<TKey> : IOutputDto
    {
        TKey Id { get; set; }
    }

    /// <summary>
    /// 实现DTO接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class OutputDtoBase<TKey> : IOutputDto<TKey>
    {
        public TKey Id { get; set; }
    }
}