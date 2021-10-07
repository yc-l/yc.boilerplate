using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core;

namespace YC.ElasticSearch
{
    public class ElasticSearchDbContext : IElasticSearchDbContext
    {

        public List<Uri> Nodes { get; set; }

        public ElasticClient Client { get; set; }

        /// <summary>
        /// 一个集群，租户共用一个集群，默认注入这一个
        /// </summary>
        /// <param name="nodesArray"></param>
        public ElasticSearchDbContext(string[] nodesArray)
        {

            Nodes = new List<Uri>();
            foreach (var s in nodesArray)
            {
                var node = new Uri(s);
                Nodes.Add(node);
            }
            var pool = new StaticConnectionPool(Nodes);
            var settings = new ConnectionSettings(pool);
            Client = new ElasticClient(settings);

        }

       

    }
}
