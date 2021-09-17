using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Neo4jXUnitTest.TestModel
{
    public class Company
    {
        public string Key { get; set; }
        public string CompanyName { get; set; }
        public string CEO { get; set; }

        public string Type { get; set; }

        public string Supervisor { get; set; }
    }
}
