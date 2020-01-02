using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uwl.Common.Cache.RedisCache;
using Uwl.Common.RabbitMQ;
using Uwl.Common.Subscription;
using Uwl.Data.EntityFramework.ButtonServices;
using Uwl.Data.EntityFramework.LogsServives;
using Uwl.Data.EntityFramework.MenuServices;
using Uwl.Data.EntityFramework.OrganizeServives;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Uwl.Data.EntityFramework.RoleServives;
using Uwl.Data.EntityFramework.ScheduleServices;
using Uwl.Data.EntityFramework.UserServices;
using Uwl.Data.Server.ButtonServices;
using Uwl.Data.Server.LogsServices;
using Uwl.Data.Server.MenuServices;
using Uwl.Data.Server.OrganizeServices;
using Uwl.Data.Server.RoleAssigServices;
using Uwl.Data.Server.RoleServices;
using Uwl.Data.Server.ScheduleServices;
using Uwl.Data.Server.UserServices;
using Uwl.Domain.ButtonInterface;
using Uwl.Domain.IRepositories;
using Uwl.Domain.LogsInterface;
using Uwl.Domain.MenuInterface;
using Uwl.Domain.OrganizeInterface;
using Uwl.Domain.RoleInterface;
using Uwl.Domain.ScheduleInterface;
using Uwl.Domain.UserInterface;
using Uwl.QuartzNet.JobCenter.Center;
using Uwl.QuartzNet.JobCenter.JobFactory;
using Uwl.ScheduledTask.Job;

namespace UwlAPI.Tools.StartupExtension
{
    /// <summary>
    /// IOC注入扩展
    /// </summary>
    public static class IOCExtension
    {
        /// <summary>
        /// 服务层注入
        /// </summary>
        /// <param name="services"></param>
        public static void ServerExtension(this IServiceCollection services)
        {
            //注入用户管理服务层实现
            services.AddScoped<IUserServer, UserServer>();
            //注入日志管理服务层实现
            services.AddScoped<ILogsServer, LogsServer>();
            //注入菜单管理服务层实现
            services.AddScoped<IMenuServer, MenuServer>();
            //注入角色管理服务层实现
            services.AddScoped<IRoleServer, RoleServer>();
            //注入按钮管理服务层实现
            services.AddScoped<IButtonServer, ButtonServer>();
            //注入菜单按钮服务层实现
            services.AddScoped<ISysMenuButtonServer, SysMenuButtonServer>();
            //注入角色权限分配服务层实现
            services.AddScoped<IRoleAssigServer, SysRoleAssigServer>();
            //注入用户角色服务层
            services.AddScoped<IUserRoleServer, UserRoleServer>();
            //注入组织机构服务层
            services.AddScoped<IOrganizeServer, OrganizeServer>();
            //注入计划任务服务层
            services.AddScoped<IScheduleServer, ScheduleServer>();
        }
        /// <summary>
        /// 仓储层注入
        /// </summary>
        /// <param name="services"></param>
        public static void RepositotyExtension(this IServiceCollection services)
        {
            //注入工作单元接口和实现
            services.AddScoped<IUnitofWork, UnitofWorkBase>();
            //注入日志管理领域仓储层实现
            services.AddScoped<ILogRepositoty, DomainLogsServer>();
            //注入菜单管理领域仓储层实现
            services.AddScoped<IMenuRepositoty, DomainMenuServer>();
            //注入角色管理领域仓储层实现
            services.AddScoped<IRoleRepositoty, DomainRoleServer>();
            //注入按钮管理领域仓储层实现
            services.AddScoped<IButtonRepositoty, DomainButtonServer>();
            //注入菜单按钮管理领域仓储层实现
            services.AddScoped<ISysMenuButton, DomainSysMenuButton>();
            //注入角色权限分配领域仓储实现
            services.AddScoped<IRoleRightAssigRepository, DomainRoleRightAssigServer>();
            //注入用户角色领域仓储实现
            services.AddScoped<IUserRoleRepository, DomainUserRoleServer>();
            //注入组织机构领域仓储实现
            services.AddScoped<IOrganizeRepositoty, DomainOrganizeServer>();
            //注入计划任务领域仓储实现
            services.AddScoped<IScheduleRepositoty, DomainScheduleServer>();
            //注入用户管理领域仓储层实现
            services.AddScoped<IUserRepositoty, DomainUserServer>();
        }
        /// <summary>
        /// 公共扩展注入
        /// </summary>
        /// <param name="services"></param>
        public static void CommonExtension(this IServiceCollection services)
        {
            //注入Redis缓存
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            //注入RabbitMQ服务
            services.AddSingleton<IRabbitMQ, RabbitServer>();
            //注入QuartzNet管理中心
            services.AddSingleton<ISchedulerCenter, SchedulerCenterServer>();
            //注入Redis消息订阅管理器
            services.AddScoped<IRedisSubscription, RedisSubscriptionServer>();
        }
        /// <summary>
        /// Job扩展注入
        /// </summary>
        /// <param name="services"></param>
        public static void JobExtension(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, IOCJobFactory>();
            services.AddTransient<TestJobOne>();//Job使用瞬时依赖注入
        }

    }
}
