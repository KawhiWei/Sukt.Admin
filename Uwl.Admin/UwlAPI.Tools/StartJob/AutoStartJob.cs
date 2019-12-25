using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Server.ScheduleServices;

namespace UwlAPI.Tools.StartJob
{
    /// <summary>
    /// 添加程序启动时自动运行所有Job是已开始执行的Job
    /// </summary>
    public  class AutoStartJob
    {
        #region MyRegion
        private readonly IScheduleServer _scheduleserver;//计划任务管理服务层
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoStartJob(IScheduleServer scheduleserver)
        {
            _scheduleserver = scheduleserver;
        }
        ///// <summary>
        ///// 获取到
        ///// </summary>
        //public void AutoJob()
        //{
        //    var Joblist=_scheduleServer.GetScheduleJobByPage(new Data.Model.Assist.ScheduleQuery { PageIndex = 1, PageSize = 50 }).Item1;
        //    var List = Joblist.Where(x=>x.IsStart==true).ToList();
        //    foreach (var item in List)
        //    {
        //        Task.Run(()=>_scheduleServer.StartJob(item.Id));//记录另一种异步写法
        //    }
        //}
        #endregion
        /// <summary>
        /// 获取到
        /// </summary>
        public  void AutoJob()
        {
            var Joblist = Task.Run(() => _scheduleserver.GetAllScheduleNotIsDrop()).Result.ToList();
            var List = Joblist.Where(x => x.IsStart == true).ToList();
            foreach (var item in List)
            {
                Task.Run(() => _scheduleserver.StartJob(item.Id));//记录另一种异步写法
            }
        }
    }
}
