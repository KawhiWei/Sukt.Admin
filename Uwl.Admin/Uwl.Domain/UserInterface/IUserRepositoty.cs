using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;

namespace Uwl.Domain.UserInterface
{
    /// <summary>
    /// 领域仓储层用户查询接口
    /// </summary>
    public interface IUserRepositoty: IRepository<SysUser>
    {
    }
    
}
