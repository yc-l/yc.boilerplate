

using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YC.Common.ShareUtils
{

    /// <summary>
    /// 压缩包，压缩，解压，支持RAR和ZIP 主要支持ZIP，目前只有解压部分的封装，之后可扩展其他功能，如：压缩
    /// </summary>
    public class SharpCompressUtils
    {
        /// <summary>
        /// zip 文件目录进行压缩
        /// </summary>
        /// <param name="filesPath">文件目录地址</param>
        /// <param name="targetDir">保存目标目录</param>
        /// <param name="compressionZipName">压缩包名称</param>
        /// <param name="error">错误信息</param>
        /// <returns>是否压缩成功</returns>
        public static bool ZIPCompression(string filesPath, string compressionZipPath,out string error) {
          
             error = "";
            bool result = false;
            try
            {
               
                using (var zip = File.OpenWrite(compressionZipPath))
                using (var zipWriter = WriterFactory.Open(zip, ArchiveType.Zip, CompressionType.Deflate))
                {
                    zipWriter.WriteAll(filesPath, "*", SearchOption.AllDirectories);
                }
                result = true;
            }
            catch (Exception ex)
            {

                error = ex.ToString();
            }
            return result;

        }

        /// <summary>
        /// zip 压缩包在线解压具体操作方法
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static bool ZIPDecompressionOperation(string sourceZIPPath, string tagetPath, out string error)
        {

            bool result = false;

            error = "";

            #region zip 文件在线解压操作

            try
            {
                var archive = ArchiveFactory.Open(sourceZIPPath);
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        // Console.WriteLine(entry.FilePath);ExtractionOptions,ExtractOptions
                        entry.WriteToDirectory(tagetPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    }
                }
            }
            catch (Exception ex)
            {

                error = ex.ToString();
            }

            result = true;

            #endregion

            return result;
        }

        /// <summary>
        /// zip 加密的压缩包在线解压具体操作方法
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static bool ZIPDecompressionOperation(string sourceZIPPath, string password, string tagetPath, out string error)
        {

            bool result = true;

            error = "";

            #region zip 文件在线解压操作

            try
            {
              
                var archive = ZipArchive.Open(sourceZIPPath, new SharpCompress.Readers.ReaderOptions() { Password=password});
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        // Console.WriteLine(entry.FilePath);
                        entry.WriteToDirectory(tagetPath,new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    }
                }
            }
            catch (Exception ex)
            {

                error = ex.ToString();
                result = false;
            }


            #endregion

            return result;
        }
    }
}
