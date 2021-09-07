using YC.Quartz.Interface;
using YC.QuartzModule.Model;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//------------------------------------------------
//   服务仓储
// 
//   author：林滨
//------------------------------------------------

namespace YC.QuartzModule
{
    public class QuartzRepository : IQuartzRepository
    {

        /// 只能使用 JobsTrigger JobKey.Name 才可以查询，如果只是JobKey 无法查询匹配
        private IScheduler scheduler;
        public List<JobsTrigger> jobTriggerServerList = new List<JobsTrigger>();//自定义 任务计划集合，做相关过滤使用比对

        public List<JobsTrigger> JobTriggerServerList
        {
            get
            {
                return jobTriggerServerList;
            }

        }

        public QuartzRepository(IScheduler DefaultScheduler)
        {

            scheduler = DefaultScheduler;
        }

        #region 加载默认插件服务
        /// <summary>
        /// 加载默认插件服务
        /// </summary>
        /// <param name="pluginDLLDir">插件服务绝对路径文件夹</param>
        public List<JobsTrigger> DefaultRunningQZServer(string pluginDLLDir, out string error)
        {
            error = "";
            try
            {
                var assemblies = new DirectoryInfo(pluginDLLDir)
                     .GetFiles("*.dll")
                     .Select(r => Assembly.LoadFrom(r.FullName)).ToArray();
                foreach (var ass in assemblies)
                {
                    #region 具体加载配个PluginDLL

                    Type[] allType = ass.GetTypes();  //获取程序集中的所有类型列表
                    Dictionary<string, Type> dic = new Dictionary<string, Type>();
                    foreach (Type temp in allType)
                    {

                        if (temp.GetInterface("ICronConfig") != null)
                        {
                            dic.Add("CronConfig", temp);
                        }
                        if (temp.GetInterface("IJobsConfig") != null)
                        {
                            dic.Add("JobsConfig", temp);
                        }

                        if (temp.GetInterface("IJobLibray") != null)
                        {
                            dic.Add("JobLibray", temp);
                        }

                    }
                    var jobsConfig = Activator.CreateInstance(dic["JobsConfig"]);
                    IJobLibray jobLibrary = (IJobLibray)Activator.CreateInstance(dic["JobLibray"]);
                    Type t = jobsConfig.GetType();

                    string tempError = "";
                    foreach (var c in jobLibrary.GetJobList())
                    {
                        AddQuartzServer(c.JobDetail, c.Trigger, out tempError);
                        error += tempError;
                        UpdateJobsTriggerList(c.JobDetail.Key);
                    }

                    #endregion
                }

                return jobTriggerServerList;
            }
            catch (Exception ex)
            {
                scheduler.Shutdown();
                jobTriggerServerList.Clear();
                error += ex.ToString();
            }
            return new List<JobsTrigger>();
        }


        #endregion

        #region 加载指定插件dll 并自定运行
        /// <summary>
        /// 加载指定插件dll 并自定运行
        /// </summary>
        /// <param name="path">指定插件dll绝对地址</param>
        /// <param name="error">错误信息</param>
        /// <returns>返回加载结果</returns>
        public bool AddQZServerDLL(string path, out string error)
        {
            error = "";
            bool result = true;
            try
            {
                #region 具体加载配个PluginDLL
                Assembly ass = Assembly.LoadFile(path);

                Type[] allType = ass.GetTypes();  //获取程序集中的所有类型列表
                Dictionary<string, Type> dic = new Dictionary<string, Type>();
                foreach (Type temp in allType)
                {

                    if (temp.GetInterface("ICronConfig") != null)
                    {
                        dic.Add("CronConfig", temp);
                    }
                    if (temp.GetInterface("IJobsConfig") != null)
                    {
                        dic.Add("JobsConfig", temp);
                    }

                    if (temp.GetInterface("IJobLibray") != null)
                    {
                        dic.Add("JobLibray", temp);
                    }

                }
                var jobsConfig = Activator.CreateInstance(dic["JobsConfig"]);
                IJobLibray jobLibrary = (IJobLibray)Activator.CreateInstance(dic["JobLibray"]);
                Type t = jobsConfig.GetType();

                string tempError = "";
                foreach (var c in jobLibrary.GetJobList())
                {
                    AddQuartzServer(c.JobDetail, c.Trigger, out tempError);
                    error += tempError;
                    UpdateJobsTriggerList(c.JobDetail.Key);
                }

                #endregion
            }
            catch (Exception ex)
            {
                result = false;
                error += ex.ToString();
            }

            return result;

        }
        #endregion

        #region  添加 任务 直接使用
        /// <summary>
        /// 添加 任务 直接使用
        /// </summary>
        /// <param name="jobDetail">任务处理模块</param>
        /// <param name="triggerList">触发器集合</param>
        /// <param name="IsRun">是否直接开启</param>
        /// <param name="error">错误消息</param>
        /// <returns>返回结果</returns>
        public bool AddQuartzServer(IJobDetail jobDetail, Quartz.Collection.HashSet<ITrigger> triggerList, out string error, bool IsRun = true)
        {
            bool result = true;
            error = "";

            try
            {
                scheduler.ScheduleJob(jobDetail, triggerList, false);
                if (IsRun)
                {
                    scheduler.ResumeJob(jobDetail.Key);
                }

            }
            catch (Exception ex)
            {

                error += ex.Message;
                result = false;

            }

            UpdateJobsTriggerList(jobDetail.Key);
            return result;


        }
        #endregion

        #region 暂停服务
        /// <summary>
        /// 暂停服务
        /// </summary>
        /// <param name="jobKey">任务Key</param>
        /// <param name="erorr">错误消息</param>
        public bool PauseQuartzServer(JobKey jobKey, out string erorr)
        {
            erorr = "";

            bool result = true;
            try
            {
                scheduler.PauseJob(jobKey);
            }
            catch (Exception ex)
            {
                erorr += ex.ToString();
                result = false;

            }
            UpdateJobsTriggerList(jobKey);
            return result;
        }
        #endregion

        #region 恢复 工作任务
        /// <summary>
        /// 恢复 工作任务
        /// </summary>
        /// <param name="jobKey"></param>
        public bool ResumeQuartzServer(JobKey jobKey)
        {
            bool result = false;
            try
            {
                scheduler.ResumeJob(jobKey);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;

            }

            UpdateJobsTriggerList(jobKey);

            return result;

        }
        #endregion

        #region 停止所有服务
        /// <summary>
        /// 停止所有服务
        /// </summary>
        /// <param name="erorr">错误消息</param>
        /// <returns></returns>
        public bool StopAllQuartzServer(out string erorr)
        {
            bool result = true;
            erorr = "";
            try
            {
                scheduler.Shutdown();       //停止调度器
            
            }
            catch (Exception ex)
            {
                erorr += ex.ToString();
                result = false;
            }
            finally
            {
                jobTriggerServerList.Clear();

            }

            return result;
        }
        #endregion

        #region 删除工作任务
        /// <summary>
        /// 删除 指定任务
        /// </summary>
        /// <param name="jobKey">任务Key</param>
        /// <returns>是否删除成功！</returns>
        public async Task<bool> DeleteQuartzServer(JobKey jobKey)
        {
            bool result =await scheduler.DeleteJob(jobKey);
            int RemoveCount = jobTriggerServerList.RemoveAll(x => x.JobKey.Name == jobKey.Name);
            return result;
        }
        #endregion

        #region 获取当前所有任务计划
        /// <summary>
        /// 获取当前所有任务计划
        /// </summary>
        /// <returns></returns>
        public List<JobsTrigger> GetALLQuartzServer()
        {
            //只返回当前快照运行服务，不返回所有的任务
            return jobTriggerServerList;
        }
        #endregion

        #region 判断是否存在指定的任务
        /// <summary>
        /// 判断是否存在指定的任务
        /// </summary>
        /// <param name="jobKey">任务key</param>
        /// <returns></returns>
        public async Task<bool> IsExistsJob(JobKey jobKey)
        {

            bool IsExist = await scheduler.CheckExists(jobKey);
            if (IsExist)
            {
                UpdateJobsTriggerList(jobKey);
            }
            return IsExist;
        }
        #endregion

        #region 判断是否存在指定的 任务以及触发器
        /// <summary>
        /// 返回是否存在指定的 任务以及触发器
        /// </summary>
        /// <param name="jobKey">任务key</param>
        /// <param name="triggerKey">触发器key</param>
        /// <returns>是否存在</returns>
        public async Task<bool> IsExistsJobsAndTrigger(JobKey jobKey, TriggerKey triggerKey)
        {
          
            bool IsExist = await scheduler.CheckExists(jobKey);
            if (IsExist)
            {

                var triggerList = await scheduler.GetTriggersOfJob(jobKey);
                if (triggerList.Where(x => x.Key.Name == triggerKey.Name).Count() > 0)
                {

                    JobsTrigger temp = new JobsTrigger();
                    temp.JobKey = jobKey;
                    temp.TriggerKey = triggerKey;
                    temp.TriggerState =await scheduler.GetTriggerState(triggerKey);
                    var localServer = jobTriggerServerList.Where(x => x.JobKey.Name == jobKey.Name && x.TriggerKey.Name == triggerKey.Name).FirstOrDefault();
                    if (localServer != null)
                    {
                        jobTriggerServerList.Remove(localServer);

                        jobTriggerServerList.Add(temp);
                    }
                    else
                    {
                        jobTriggerServerList.Add(localServer);
                    }
                    return true;
                }
                else
                {

                    return false;
                }
            }
            else
            {

                return IsExist;
            }


        }
        #endregion

        #region  自动更新 任务触发器集合
        /// <summary>
        /// 自动更新 任务触发器集合
        /// </summary>
        /// <param name="jobKey">任务key</param>
        protected async Task UpdateJobsTriggerList(JobKey jobKey)
        {

            if (jobTriggerServerList.Where(x => x.JobKey.Name == jobKey.Name).Count() > 0)
            {
                jobTriggerServerList.RemoveAll(x => x.JobKey.Name == jobKey.Name);
            }
            var jobCollection= await scheduler.GetTriggersOfJob(jobKey);
            foreach (var v in jobCollection)
            {
                var temp = new JobsTrigger();
                temp.JobKey = jobKey;
                temp.TriggerKey = v.Key;
                temp.TriggerState =await scheduler.GetTriggerState(v.Key);
                jobTriggerServerList.Add(temp);
            }

        }
        #endregion

    }
}
