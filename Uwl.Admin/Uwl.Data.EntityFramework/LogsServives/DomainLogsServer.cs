using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.LogsInterface;

namespace Uwl.Data.EntityFramework.LogsServives
{
    /// <summary>
    /// 定义一个Logs的接口实现分别继承基类实现和Logs接口
    /// </summary>
    public class DomainLogsServer : CoreRepositoryBase<Logs>, ILogRepositoty
    {
        public readonly Domain.IRepositories.IRepository _repository;
        public DomainLogsServer(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
    }
}
