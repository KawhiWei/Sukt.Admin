using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.RoleInterface;

namespace Uwl.Data.EntityFramework.RoleServives
{
    /// <summary>
    /// 角色权限领域层
    /// </summary>
    public class DomainRoleRightAssigServer: CoreRepositoryBase<SysRoleRight>, IRoleRightAssigRepository
    {
        public DomainRoleRightAssigServer(IUnitofWork unitofWork) : base(unitofWork)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InsertRoleAssig">新权限</param>
        /// <param name="DeleteRoleAssig">删除旧权限</param>
        /// <returns></returns>
        public bool SaveRoleAssigByTrans(List<SysRoleRight> InsertRoleAssig, List<SysRoleRight> DeleteRoleAssig)
        {
            using (var trans = _uwldbContext.Database.BeginTransaction())
            {
                try
                {
                    _uwldbContext.RemoveRange(DeleteRoleAssig);//删除之前旧的权限
                    _uwldbContext.Set<SysRoleRight>().AddRange(InsertRoleAssig);//添加新的权限数据进去
                    _uwldbContext.SaveChanges();
                    trans.Commit();//提交事务
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
