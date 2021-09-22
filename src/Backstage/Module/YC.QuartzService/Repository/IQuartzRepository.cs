using YC.QuartzServiceModule.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using YC.QuartzService.Model;
using YC.QuartzService.Interface;
//------------------------------------------------
//   服务仓储接口
// 
//   author：林宣名
//------------------------------------------------
namespace YC.QuartzServiceModule
{
    public interface IQuartzRepository
    {

        List<JobsTrigger> JobTriggerServerList { get; }


        /// <summary>
        /// 加载默认服务 采用对象集合
        /// </summary>
        /// <param name="jobLibrayList"></param>
        /// <returns></returns>
        Task<List<JobsTrigger>> DefaultRunningServer(List<QuartzJobsCollection> jobLibrayList);
        /// <summary>
        /// 加载指定插件dll 并自定运行
        /// </summary>
        /// <param name="path">指定插件dll绝对地址</param>
        /// <param name="error">错误信息</param>
        /// <returns>返回加载结果</returns>
        //Task<WorkResult> AddQZServerDLL(string path);

        /// <summary>
        /// 添加 任务 直接使用
        /// </summary>
        /// <param name="jobDetail">任务处理模块</param>
        /// <param name="triggerList">触发器集合</param>
        /// <param name="IsRun">是否直接开启</param>
        /// <param name="error">错误消息</param>
        /// <returns>返回结果</returns>
        Task<WorkResult> AddQuartzServer(IJobDetail jobDetail, HashSet<ITrigger> triggerList,  bool IsRun = true);



        /// <summary>
        /// 暂停服务
        /// </summary>
        /// <param name="jobKey">任务Key</param>
        /// <param name="erorr">错误消息</param>
        Task<WorkResult> PauseQuartzServer(JobKey jobKey);



        /// <summary>
        /// 恢复 工作任务
        /// </summary>
        /// <param name="jobKey">工作Key</param>
        Task<WorkResult> ResumeQuartzServer(JobKey jobKey);

        /// <summary>
        /// 停止所有服务
        /// </summary>
        /// <returns></returns>
        Task<WorkResult> StopAllQuartzServer();




        /// <summary>
        /// 删除 指定任务
        /// </summary>
        /// <param name="jobKey">任务Key</param>
        /// <returns>是否删除成功！</returns>
        Task<bool> DeleteQuartzServer(JobKey jobKey);


        /// <summary>
        /// 获取当前所有任务计划
        /// </summary>
        /// <returns></returns>
        List<JobsTrigger> GetALLQuartzServer();



        /// <summary>
        /// 判断是否存在指定的任务
        /// </summary>
        /// <param name="jobKey">任务key</param>
        /// <returns></returns>
        Task<bool> IsExistsJob(JobKey jobKey);

        /// <summary>
        /// 返回是否存在指定的 任务以及触发器
        /// </summary>
        /// <param name="jobKey">任务key</param>
        /// <param name="triggerKey">触发器key</param>
        /// <returns>是否存在</returns>
        Task<bool> IsExistsJobsAndTrigger(JobKey jobKey, TriggerKey triggerKey);




    }
}
