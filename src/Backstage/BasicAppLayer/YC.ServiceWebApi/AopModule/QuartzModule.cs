using Autofac;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core.Autofac;
using YC.QuartzService.Interface;
using YC.QuartzService.JobService.CreateDirJobService;
using YC.QuartzService.JobService.DeleteLogJobService;
using YC.QuartzService.JobService.WriteFileJobService;
using YC.QuartzServiceModule;

namespace YC.ServiceWebApi.AopModule
{
    /// <summary>
    /// QuartzModule 注入模块
    /// </summary>
    [DependsOn]
    public class QuartzModule : Autofac.Module
    {

        /// <summary>
        ///  QuartzModule 注入模块操作
        /// </summary>
        /// <param name="builder"></param>
        protected async override void Load(ContainerBuilder builder)
        {
           var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            builder.RegisterInstance(scheduler).SingleInstance();//单例注册 
            builder.RegisterType<QuartzRepository>().As<IQuartzRepository>().SingleInstance();//单例注册 
        }
    }
}
