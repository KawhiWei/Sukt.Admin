using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Dtos.ShoopingCart
{
    public class Product
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }
    }
}
