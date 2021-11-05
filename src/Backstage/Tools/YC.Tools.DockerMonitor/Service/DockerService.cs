using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace YC.Tools.DockerMonitor
{


        
    public  class DockerService:IDisposable
    {

        public SshClient _sshClient;
        public SshConfig _sshConfig;
        public DockerService(SshConfig sshConfig) {
            _sshConfig = sshConfig;
            Init();
        }
        public void Init() {

            _sshClient = new SshClient(_sshConfig.Host, _sshConfig.Port, _sshConfig.UserName, _sshConfig.Password);
            _sshClient.Connect();
        }
     
        public void DockerConatainsStatsMonitor(Action dockerWork=null)
        {
            //列出操作列表
            Console.WriteLine($"是否默认显示当前docker 容器列表？ Y/N");

            string cmd = Console.ReadLine();
            if (cmd.ToLower() == "y")
            {
                Console.WriteLine($"docker 容器列表"); 
                Console.WriteLine(Excute("docker ps -a"));
            }
            else if (cmd.ToLower() == "n")
            {
                Console.Write($"{_sshConfig.Title}:");

            }
            Console.Write($"请输入docker 容器 id:");
            string dockerContainId = Console.ReadLine();

            string dockerStatsCmd = DockerCmd.ContainsStats(dockerContainId);
           
            ExcultWhileConsole(dockerStatsCmd);
            #region 定时器处理

            //System.Timers.Timer timer = new System.Timers.Timer(1000);
            //timer.Elapsed +=  (sender, e) => {
            // Console.WriteLine(new Random(DateTime.Now.Millisecond).Next(0, 10000));
            //var sshCommand = _sshClient.RunCommand("docker stats -a  " + dockerId);
            //Console.Write(sshCommand.Execute());
            //var res = Excute("docker stats -a  " + dockerId);

            //if (!string.IsNullOrWhiteSpace(res))
            //{
            //    Console.WriteLine(res);
            //}
            //};
            // timer.Start(); 
            #endregion

        }

        public void DockerContainLogsMonitor(string dockerContainId) {

            ExcultWhileConsole(DockerCmd.ContainsLogs(dockerContainId));
        }

        public void ExcultWhileConsole(string cmd) {

            var clientCmd = _sshClient.CreateCommand(cmd);   //  very long list
            var asynch = clientCmd.BeginExecute();
            var reader = new StreamReader(clientCmd.OutputStream);

            while (!asynch.IsCompleted)
            {
                var result = reader.ReadToEnd();
                if (string.IsNullOrEmpty(result))
                    continue;
                Console.Write(result);
            }
            clientCmd.EndExecute(asynch);
        }

        public string Excute(string cmd)
        {
            string res = "";
            using (var sshCmd = _sshClient.CreateCommand(cmd))
            {
              
                 res = sshCmd.Execute(); 
            }
            return res;
        }
      
        private bool IsDisposed = false;

        protected void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing) {

                    if (_sshClient != null)
                    {
                        if (_sshClient.IsConnected)
                        {
                            _sshClient.Disconnect();
                        }
                       
                    }
                    _sshClient.Dispose();
                }
                

            }
            IsDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            //这个执行调用了，那么析构函数就不执行
            GC.SuppressFinalize(this);
        }

         ~DockerService() {

            Dispose(false);
        }
    }
}
