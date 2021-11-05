using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Tools.DockerMonitor
{
   public class SshConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Url {

            get { 
            return this.Host+":"+this.Port;
            }
        }

        public string Title {
            get
            {
                return this.UserName + "@" + this.Url;
            }

        }
    }
}
