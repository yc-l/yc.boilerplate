using YC.QuartzModule.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//------------------------------------------------
//   服务仓储接口
// 
//   author：林滨
//------------------------------------------------
namespace YC.QuartzModule
{
    public interface IQuartzRepository
    {

        List<JobsTrigger> JobTriggerServerList { get; }


        /// <summary>
        /// 加载默认插件服务
        /// </summary>
        /// <param name="pluginDLLDir">插件服务绝对路径文件夹</param>
        List<JobsTrigger> DefaultRunningQZServer(string pluginDLLDir, out string error);

          /// <summary>
          /// 加载指定插件dll 并自定运行
          /// </summary>
          /// <param name="path">指定插件dll绝对地址</param>
          /// <param name="error">错误信息</param>
          /// <returns>返回加载结果</returns>
       bool AddQZServerDLL(string path, out string error);

        /// <summary>
        /// 添加 任务 直接使用
        /// </summary>
        /// <param name="jobDetail">任务处理模块</param>
        /// <param name="triggerList">触发器集合</param>
        /// <param name="IsRun">是否直接开启</param>
        /// <param name="error">错误消息</param>
        /// <returns>返回结果</returns>
        bool AddQuartzServer(IJobDetail jobDetail, Quartz.Collection.HashSet<ITrigger> triggerList, out string error, bool IsRun = true);

       

        /// <summary>
        /// 暂停服务
        /// </summary>
        /// <param name="jobKey">任务Key</param>
        /// <param name="erorr">错误消息</param>
        bool PauseQuartzServer(JobKey jobKey, out string erorr);



        /// <summary>
        /// 恢复 工作任务
        /// </summary>
        /// <param name="jobKey">工作Key</param>
        bool ResumeQuartzServer(JobKey jobKey);




        /// <summary>
        /// 停止所有服务
        /// </summary>
        /// <param name="erorr">错误消息</param>
        /// <returns></returns>
        bool StopAllQuartzServer(out string erorr);




        /// <summary>
        /// 删除 指定任务
        /// </summary>
        /// <param name="jobKey">任务Key</param>
        /// <returns>是否删除成功！</returns>
        bool DeleteQuartzServer(JobKey jobKey);


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
        bool IsExistsJob(JobKey jobKey);

        /// <summary>
        /// 返回是否存在指定的 任务以及触发器
        /// </summary>
        /// <param name="jobKey">任务key</param>
        /// <param name="triggerKey">触发器key</param>
        /// <returns>是否存在</returns>
        bool IsExistsJobsAndTrigger(JobKey jobKey, TriggerKey triggerKey);




    }
}
