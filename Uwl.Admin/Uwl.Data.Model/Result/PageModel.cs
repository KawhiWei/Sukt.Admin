using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.Result
{
    public class PageModel<T>
    {
        public int TotalCount{ get; set; } = 0;
        public List<T> data{ get; set; } 
    }
}
