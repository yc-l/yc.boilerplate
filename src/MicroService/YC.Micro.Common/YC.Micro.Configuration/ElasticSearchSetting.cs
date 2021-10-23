using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Micro.Configuration
{
    public class ElasticSearchSetting
    {
        public List<ElasticSearchNode> Nodes { get; set; }
    }

    public class ElasticSearchNode
    {

        public string Node { get; set; }
    }
}
