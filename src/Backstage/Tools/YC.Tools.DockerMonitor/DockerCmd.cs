using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Tools.DockerMonitor
{
  public  class DockerCmd
    {
        /// <summary>
        ///显示docker 监控
        /// </summary>
        /// <param name="dockerContainId"></param>
        /// <returns></returns>
        public static string ContainsStats(string dockerContainId)=>$"docker stats -a  {dockerContainId}";

        /// <summary>
        /// 显示docker内部日志
        /// </summary>
        /// <param name="dockerContainId"></param>
        /// <returns></returns>
        public static string ContainsLogs(string dockerContainId) => $"docker logs  {dockerContainId}";
    }
}
