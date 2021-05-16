
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YC.Common.ShareUtils;

namespace YC.Common
{
    /// <summary>
    /// 1 制造了一个 错误消息的（内存）队列。当出现异常之后，
    /// 直接把错误消息放到内存的队列里去。
    /// 然后Web继续往下执行，给用户反馈信息。不会阻塞用户的响应。
    /// 2 用户了Log4net可以将日志写到不同地方去。不需要改代码。
    /// </summary>
    public class LogUtils
    {
        //异常信息的队列
        public static Queue<LogDto> logInfoQueue;

        public static bool isWork = false;//是否工作标识

        //如果调用方也是线程池，会出现等待对方完成后，才会执行这个反复，对方如果是多线程，那么这边不会影响
        static  LogUtils()
        {
            isWork = true;
            logInfoQueue = new Queue<LogDto>();
            ThreadPool.QueueUserWorkItem(u =>
                {
                    while (true)
                    {
                        var logDto = new LogDto();

                        if (logInfoQueue == null)
                        {
                            logInfoQueue = new Queue<LogDto>();  
                            continue;
                        }

                        lock (logInfoQueue)
                        {
                            if (logInfoQueue.Count > 0)
                                logDto = logInfoQueue.Dequeue();
                        }
                        //往日志文件里面写就可以了。
                        if (!string.IsNullOrEmpty(logDto.Message))
                        {
                            //log4net.Config.XmlConfigurator.Configure();//把当前的log4net的配置起作用。

                            //log4net.ILog log = log4net.LogManager.GetLogger(DateTime.Now.ToString());
                            //log.Error("Error", new Exception(str));
                            string logDir = Environment.CurrentDirectory + $@"\Logs\{ DateTime.Now.ToString("yyyy-MM-dd")}\";
                            bool isCreatedDir = CreateDir(logDir);

                            string filePath = logDir + logDto.TypeName + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                          
                            bool isWrited = AppendWriteFile(filePath, logDto.Message);

                        }
                        if (logInfoQueue.Count() <= 0)
                        {
                            Thread.Sleep(30);
                        }


                    }
                });
        }   

        public static void WriteLog(LogDto logInput)
        {
           
            lock (logInfoQueue)
            {
                logInfoQueue.Enqueue(logInput);
            }

        }

      

        #region 文件操作
        /// <summary>
        /// 文件读取操作
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="fileContent">文件读取内容</param>
        /// <param name="error">错误消息</param>
        /// <returns>是否正常读取</returns>
        public static bool ReadFile(string filePath, out string fileContent, string error = null)
        {
            error = "";
            fileContent = "";
            #region 文件读取
            try
            {
                string path = filePath;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader TempReader = new StreamReader(fs, Encoding.UTF8);
                string template = TempReader.ReadToEnd();

                if (template.Trim().Length < 0)
                {
                    return false;
                }
                fileContent = template;

                TempReader.Close();
            }
            catch (Exception ex)
            {

                error += ex.ToString();
            }
            #endregion
            return true;

        }


        /// <summary>
        /// 文件写入操作(追加写入)
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="writeContent">写入内容</param>
        /// <param name="error">错误内容</param>
        /// <returns>是否写入</returns>
        public static bool AppendWriteFile(string filePath, string writeContent, string error = null)
        {

            error = "";
            #region 文件写入操作
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, System.Text.ASCIIEncoding.UTF8);
                string textContent = writeContent;
                sw.WriteLine(textContent);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                error += ex.ToString();
                return false;
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹绝对路径</param>
        /// <returns></returns>
        public static bool CreateDir(string path)
        {

            bool isExists = false;
            if (!Directory.Exists(path))//判断文件夹是否存在 
            {
                Directory.CreateDirectory(path);//不存在则创建文件夹 
            }

            if (Directory.Exists(path))//判断文件夹是否存在 
            {
                isExists = true;
            }
            return isExists;
        }
        #endregion
    }

    public class LogDto
    {


        public string Message { get; set; }
        public string TypeName { get; set; }

    }
}
