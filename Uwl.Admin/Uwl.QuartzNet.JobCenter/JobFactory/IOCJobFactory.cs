using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.QuartzNet.JobCenter.JobFactory
{
    public class IOCJobFactory : IJobFactory
    {
        /// <summary>
        /// 注入反射获取依赖对象
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        public IOCJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 实现接口Job
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                //个人测试目前没有内存泄漏等问题，若是各位大佬有上生产环境的  请监控一下内存情况
                var serviceScope = _serviceProvider.CreateScope();
                var job = serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
                //var job = _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                //return job;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if(disposable!=null)
            {
                disposable.Dispose();
            }
            
        }
    }
}
