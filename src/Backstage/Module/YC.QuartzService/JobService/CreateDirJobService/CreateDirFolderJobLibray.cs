
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YC.QuartzService.Interface;
using YC.QuartzServiceModule.Model;
//------------------------------------------------
//   服务资源池
// 
//   author：林宣名
//------------------------------------------------
namespace YC.QuartzService.JobService.CreateDirJobService
{
    public class CreateDirFolderJobLibray : IJobLibray
    {
        public List<QuartzJobsCollection> GetJobList()
        {
            List<QuartzJobsCollection> jobList = new List<QuartzJobsCollection>();

            CreateDirFolderJobsConfig jobsConfig = new CreateDirFolderJobsConfig();

            Type t = jobsConfig.GetType();

            System.Reflection.PropertyInfo[] properties = t.GetProperties();
            foreach (System.Reflection.PropertyInfo s in t.GetProperties())
            {
                jobList.Add((QuartzJobsCollection)jobsConfig.GetType().GetProperty(s.Name).GetValue(jobsConfig, null));
            }

            return jobList;


        }
    }
}
