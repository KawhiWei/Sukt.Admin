using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    /// <summary>
    /// 定义输入DTO底层接口
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public interface IInputDto<Tkey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        Tkey Id { get; set; }
    }
}
