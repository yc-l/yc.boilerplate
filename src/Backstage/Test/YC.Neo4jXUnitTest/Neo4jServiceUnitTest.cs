using System;
using System.Threading.Tasks;
using Xunit;
using YC.Neo4j;
using YC.Neo4jXUnitTest.TestModel;

namespace YC.Neo4jXUnitTest
{
    public class Neo4jServiceUnitTest
    {
        Neo4jService neo4jService;

        public Neo4jServiceUnitTest()
        {
            neo4jService = new Neo4jService("testdb");
        }
        [Fact]
        public async Task CreateTest()
        {
            UserInfo u1 = new UserInfo();
            u1.Key = Guid.NewGuid().ToString();
            u1.Name = "张小妞";
            u1.Sex = "女";
            u1.Remark = "高级用户";
            var result = await neo4jService.CreateSingleNode<UserInfo>("UserInfo", u1);
            Assert.Equal(1,result.Counters.NodesCreated);
        }
        [Fact]
        public async Task MatchRelationTest()
        {
            string condition = "a.Remark=b.Remark and a.Key<>b.Key";
            var result = await neo4jService.MatchNodeByProperty("UserInfo", "UserInfo", "sameRemark", "a.Remark", condition);
            Assert.True(result.Counters.ContainsUpdates);
        }

        [Fact]
        public async Task UpateNodeTest()
        {
            string condtion = "Name:'王小玉',Sex:'女'";
            string setStr = "n.Name='王小玉',n.Remark='普通用户'";
            var result = await neo4jService.UpdateNode("UserInfo", condtion, setStr);

            Assert.True(result.Counters.ContainsUpdates);
            Assert.Equal(2, result.Counters.PropertiesSet);
        }

        [Fact]
        public async Task SelectNodeTest()
        {
            string condtion = "n.Sex='女'";
          var list= await neo4jService.SelectNode<UserInfo>("UserInfo", condtion);

            Assert.True(list.Count > 0);
           
        }
    }
}
