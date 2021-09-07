using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//------------------------------------------------
//  演示任务 简单日志写入操作
// 
//   author：林宣名
//------------------------------------------------
namespace YC.QuartzService.JobService.WriteFileJobService
{
   public class LogHelper
    {
       public static void WriteLog(string LogContent) {

           string dirPath = System.Environment.CurrentDirectory + "\\File";//文件夹路径
           string path = System.Environment.CurrentDirectory + @"\File\log.txt";
           if (!Directory.Exists(dirPath))//判断文件夹是否存在
           {
               Directory.CreateDirectory(dirPath);//不存在则创建文件夹
           }

           if (File.Exists(path)==false)
           {
                FileInfo file = new FileInfo(path);
                //创建文件
                FileStream fsc = file.Create();
                //关闭文件流
                fsc.Close();

            }

           FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
           StreamWriter sw = new StreamWriter(fs, System.Text.ASCIIEncoding.UTF8);
           string textContent = LogContent;
           sw.WriteLine(textContent);
           sw.Flush();
           sw.Close();
       
       }
    }
}
