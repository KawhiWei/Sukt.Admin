using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    /// <summary>
    /// 访问资源配置
    /// </summary>
    [DisplayName("访问资源配置")]
    public class SuktResourceScopeModel
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [DisplayName("资源名称")]
        public string Name { get; set; }
        /// <summary>
        /// 资源显示名称
        /// </summary>
        [DisplayName("资源显示名称")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        [DisplayName("属性")]
        public string Properties { get; set; }
        /// <summary>
        /// 资源域
        /// </summary>
        [DisplayName("资源域")]
        public ICollection<string> Resources { get; set; }
        /// <summary>
        /// 获取、设置并发标记
        /// </summary>
        [DisplayName("获取、设置并发标记")]
        public string ConcurrencyToken { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
    }
}
