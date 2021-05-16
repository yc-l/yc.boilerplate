using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace YC.Common.ShareUtils
{
   public class PortUtils
    {
       /// <summary>
       /// 测试端口是否被占用
       /// </summary>
       /// <param name="port"></param>
       /// <returns></returns>
       public static bool PortInUse(int port)
       {
           bool inUse = false;

           IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
           IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

           foreach (IPEndPoint endPoint in ipEndPoints)
           {
               if (endPoint.Port == port)
               {
                   inUse = true;
                   break;
               }
           }
           return inUse;
       }
    }
}
