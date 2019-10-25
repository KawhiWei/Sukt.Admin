using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.RoleAssigVO
{
    public class SaveRoleAssigViewModel
    {
        public Guid RoleId { get; set; }
        public string menuIds { get; set; }
        public string BtnIds { get; set; }
        public string CreatedName { get; set; }
        public Guid CreatedId { get; set; }
    }
}
