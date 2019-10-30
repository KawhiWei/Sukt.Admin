using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.IRepositories;
using Uwl.Domain.ScheduleInterface;

namespace Uwl.Data.EntityFramework.ScheduleServices
{
    public class DomainScheduleServer: CoreRepositoryBase<SysSchedule>, IScheduleRepositoty
    {
        public DomainScheduleServer(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
    }
}
