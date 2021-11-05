using System;

namespace YC.Tools.DockerMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //SshService.Test();
            string host = "";
            int port = 22;
            string userName = "root";
            string pwd = "";
            var service = new DockerService(new SshConfig() { Host = host, Port = port, UserName = userName, Password = pwd });
            Console.WriteLine($"连接主机{host}:{port}成功.");
            service.DockerConatainsStatsMonitor();
         
            Console.ReadKey();
        }
    }
}
