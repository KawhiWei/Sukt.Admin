using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uwl.Data.Model.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public enum OrganizeEnum
    {
        [Display(Name = "单位")]
        Company =1,
        [Display(Name = "部门")]
        DepartMent=2,
    }
}
