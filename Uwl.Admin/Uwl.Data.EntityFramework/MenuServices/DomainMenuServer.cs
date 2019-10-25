using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.MenuInterface;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Uwl.Domain.IRepositories;

namespace Uwl.Data.EntityFramework.MenuServices
{
    /// <summary>
    /// 菜单管理领域层实现
    /// </summary>

    public class DomainMenuServer: CoreRepositoryBase<SysMenu>, IMenuRepositoty
    {
        /// <summary>
        /// 注入接口上下文对象
        /// </summary>
        /// <param name="uwlDbContext"></param>
        public DomainMenuServer(IUnitofWork unitofWork) : base(unitofWork)
        {
        }
        //Sql语句查询
        //_unitofWork.SqlQuery<SysMenu>("select * from SysMenu where Id=@Id",new Dictionary<string, object> {
        //    { "Id",sysMenu.Id},
        //    { "Id",sysMenu.Id},
        //    { "Id",sysMenu.Id},
        //    { "Id",sysMenu.Id},
        //});
    }
}
