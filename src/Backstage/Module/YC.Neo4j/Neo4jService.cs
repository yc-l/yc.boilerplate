
using Neo4j.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using YC.Common.ShareUtils;
using System.Linq;
namespace YC.Neo4j
{
    public class Neo4jService
    {

        private IDriver driver;
        private IAsyncSession session;

        public Neo4jService(string dbName, string uri = "neo4j://localhost:7687", string user = "neo4j", string password = "123456")
        {
            driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            session = driver.AsyncSession();
        }



        /// <summary>
        /// 创建 节点名称为n,节点表：lable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lable"></param>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultSummary> CreateSingleNode<T>(string lable, T input) where T : class, new()
        {
            var inputProperties = input.GetType().GetProperties();
            string propertiesStr = "";
            foreach (var v in inputProperties)
            {
                string propertyBuilder = $"{v.Name}:\"{Convert.ToString(v.GetValue(input, null))}\",";
                propertiesStr += propertyBuilder;
            }
            bool isExist = propertiesStr.IndexOf(',', propertiesStr.Length - 1) > 0;
            if (isExist)
            {
                propertiesStr = propertiesStr.Remove(propertiesStr.Length - 1, 1);
            }

            string graphStr = $"CREATE (n:{lable} {{ {propertiesStr}}}) RETURN n";
            IResultCursor cursor = await session.RunAsync(graphStr);
            IResultSummary result = await cursor.ConsumeAsync();
            return result;


        }
        /// <summary>
        /// 添加数据关联
        /// </summary>
        /// <param name="labelA">表1</param>
        /// <param name="labelB">表2</param>
        /// <param name="relationName">关系名称</param>
        /// <param name="relationKey">指定关系key</param>
        /// <param name="condition">a.objName = b.objName and a.objId<> b.objId</param>
        /// <returns></returns>
        public async Task<IResultSummary> MatchNodeByProperty(string labelA, string labelB, string relationName, string relationKey, string condition)
        {

            string graphStr = $"MATCH (a:{labelA}), (b:{labelB}) " +
                $"WHERE {condition} " +
                $" MERGE(a) -[:{relationName}{{ relationKey: {relationKey}}}]->(b)";
            IResultCursor cursor = await session.RunAsync(graphStr);
            IResultSummary result = await cursor.ConsumeAsync();
            return result;
        }

        /// <summary>
        /// 更新节点数据
        /// </summary>
        /// <param name="label">表名</param>
        /// <param name="setStr"> n.fileNo = 'aaa'</param>
        /// <returns></returns>

        public async Task<IResultSummary> UpdateNode(string label,string condition,string setStr,string defaultKey="n") {

            string graphStr = $"match({defaultKey}: {label}{{ {condition} }}) set {setStr} return {defaultKey}";
            IResultCursor cursor = await session.RunAsync(graphStr);
            IResultSummary result = await cursor.ConsumeAsync();
            return result;
        }

        /// <summary>
        /// 查询指定节点
        /// </summary>
        /// <typeparam name="T">需要转化的属性</typeparam>
        /// <param name="label">表</param>
        /// <param name="condition">条件</param>
        /// <param name="defaultKey">n 类似 lambda 语法糖中的x等</param>
        /// <returns></returns>
        public async Task<List<T>> SelectNode<T>(string label, string condition, string defaultKey = "n") where T:class,new()
        {
            IResultCursor resultCursor;
            List<IRecord> records=new List<IRecord>();
            string graphStr = $"match({defaultKey}: {label}) where {condition} return {defaultKey}";
             await session.ReadTransactionAsync(async txc => {
                  records = await txc.RunAsync(graphStr)
                            .ContinueWith(r => r.Result.ToListAsync())
                            .Unwrap();
             });
           var nodeList= records.Select(r => r[0].As<INode>());
            List<T> list = new List<T>();
           
            foreach (var r in nodeList) {

                list.Add(JsonConvert.SerializeObject(r.Properties).ToObject<T>());
            }

            return list;

        }

        public async Task Dispose()
        {
            await session.CloseAsync();
            await driver.CloseAsync();
            GC.SuppressFinalize(this);
        }



    }

}
