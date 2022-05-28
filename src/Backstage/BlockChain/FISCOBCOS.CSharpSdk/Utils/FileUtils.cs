
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;


namespace FISCOBCOS.CSharpSdk.Utils
{

    public class FileUtils
    {
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
    }

}


