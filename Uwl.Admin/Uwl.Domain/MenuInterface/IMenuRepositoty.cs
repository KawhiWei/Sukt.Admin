using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;

namespace Uwl.Domain.MenuInterface
{
    /// <summary>
    /// 菜单管理领域层接口定义
    /// </summary>
    public interface IMenuRepositoty : IRepository<SysMenu>
    {
    }
}
