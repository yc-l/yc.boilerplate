using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YC.Neo4j;
using YC.Neo4jXUnitTest.TestModel;

namespace YC.Neo4jXUnitTest
{
    public class Neo4jServiceUnitTest
    {
        public Neo4jRepository neo4jRepository;
        public List<UserInfo> userList;
        public List<Company> companyList;
        public Neo4jServiceUnitTest()
        {
            neo4jRepository = new Neo4jRepository("testdb");
            userList = new List<UserInfo>();
            userList.Add(new UserInfo() { Key = Guid.NewGuid().ToString(), Name = "张三", Sex = "男", Type = "普通用户" });
            userList.Add(new UserInfo() { Key = Guid.NewGuid().ToString(), Name = "里斯", Sex = "男", Type = "高级用户" });
            userList.Add(new UserInfo() { Key = Guid.NewGuid().ToString(), Name = "王五", Sex = "男", Type = "普通用户" });
            userList.Add(new UserInfo() { Key = Guid.NewGuid().ToString(), Name = "张小玉", Sex = "女", Type = "高级用户" });
            companyList = new List<Company>();
            companyList.Add(new Company() { Key = Guid.NewGuid().ToString(), CompanyName = "万度科技", CEO = "张三", Supervisor = "张小玉", Type = "科技" });
            companyList.Add(new Company() { Key = Guid.NewGuid().ToString(), CompanyName = "签谷科技", CEO = "王五", Supervisor = "张小玉", Type = "科技" });
            companyList.Add(new Company() { Key = Guid.NewGuid().ToString(), CompanyName = "东方娱乐", CEO = "张小玉", Supervisor = "张小小", Type = "娱乐" });
            companyList.Add(new Company() { Key = Guid.NewGuid().ToString(), CompanyName = "杰飞实业", CEO = "里斯", Supervisor = "张小小", Type = "实业" });


        }

        /// <summary>
        /// 创建一条记录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateTest()
        {
            UserInfo u1 = new UserInfo();
            u1.Key = Guid.NewGuid().ToString();
            u1.Name = "赵小";
            u1.Sex = "女";
            u1.Type = "高级用户";

            var result = await neo4jRepository.CreateSingleNode<UserInfo>("UserInfo", u1);

            Assert.Equal(1, result.Counters.NodesCreated);
        }

        /// <summary>
        /// 创建默认测试数据
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateDataTest()
        {
            bool state = true;
            try
            {
                foreach (var u in userList)
                {
                    await neo4jRepository.CreateSingleNode<UserInfo>("UserInfo", u);
                }
                foreach (var c in companyList)
                {
                    await neo4jRepository.CreateSingleNode<Company>("Company", c);
                }
            }
            catch (Exception ex)
            {

                state = false;
            }
            Assert.True(state);
        }

        /// <summary>
        /// 创建 掌控 关联数据关联
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task MatchRelationControlTest()
        {
            string condition = $"{neo4jRepository.LeftKey}.Name={neo4jRepository.RightKey}.CEO";
            var result = await neo4jRepository.MatchNodeByProperty("UserInfo", "Company", "掌权", $"{neo4jRepository.LeftKey}.Name", condition);
            Assert.True(result.Counters.ContainsUpdates);
        }

        /// <summary>
        /// 创建 公司关联 关联数据关联
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task MatchRelationLinkTest()
        {
            string condition = $"{neo4jRepository.LeftKey}.Name={neo4jRepository.RightKey}.Supervisor";
            var result = await neo4jRepository.MatchNodeByProperty("UserInfo", "Company", "公司关联", $"{neo4jRepository.LeftKey}.Name", condition);
            Assert.True(result.Counters.ContainsUpdates);
        }

        /// <summary>
        /// 更新节点信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpateNodeTest()
        {
            string condtion = "Name:'里斯'";
            string setStr = $"{neo4jRepository.Key}.Name='里斯李',{neo4jRepository.Key}.Type='VIP用户'";
            var result = await neo4jRepository.UpdateNode("UserInfo", condtion, setStr);

            Assert.True(result.Counters.ContainsUpdates);
            Assert.Equal(2, result.Counters.PropertiesSet);
        }

        /// <summary>
        /// 查询指定节点信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SelectNodeTest()
        {
            string condtion = $"{neo4jRepository.Key}.Name='张小玉'";
            var list = await neo4jRepository.SelectNode<UserInfo>("UserInfo", condtion);
            Assert.True(list.Count > 0);

        }

        /// <summary>
        /// 通过连接关系 查询指定节点信息
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SelectNodeByRelationShipTest()
        {
            string relationShipName = "公司关联";
            string condition = "UserInfo.Name='张小玉'";
            var tupleList = await neo4jRepository.SelectNodeByRelationShoip<UserInfo,Company>("UserInfo", "Company",relationShipName, condition);
            Assert.True(tupleList.Item1.Count>0);
            Assert.True(tupleList.Item2.Count>0);

        }

        [Fact]
        public async Task DeleteNodeTest()
        {
            string condtion = $"{neo4jRepository.Key}.Sex='男'";
            var result = await neo4jRepository.DeleteNode("UserInfo", condtion, true);
            Assert.True(result.Counters.NodesDeleted == 1);
        }
    }
}
