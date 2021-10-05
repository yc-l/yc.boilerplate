using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ElasticSearch
{
    public interface IElasticSearchDbContext
    {


        public List<Uri> Nodes { get; set; }

        public ElasticClient Client { get; set; }

    }
}
