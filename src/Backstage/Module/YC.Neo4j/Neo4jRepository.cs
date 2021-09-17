
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
    public class Neo4jRepository
    {

        private IDriver driver;
        private IAsyncSession session;

        private const string defaultKey = "n";//默认 代指表等 类似lambda x=> 中的x
        private const string defaultLeftKey = "a";
        private const string defaultRightKey = "b";
        public Neo4jRepository(string dbName, string uri = "neo4j://localhost:7687", string user = "neo4j", string password = "123456")
        {
            driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            session = driver.AsyncSession();
        }

        public string Key
        {

            get
            {
                return defaultKey;
            }

        }
        public string LeftKey
        {

            get
            {
                return defaultLeftKey;
            }

        }
        public string RightKey
        {

            get
            {
                return defaultRightKey;
            }

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

            string graphStr = $"CREATE ({defaultKey}:{lable} {{ {propertiesStr}}}) RETURN {defaultKey}";
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

            string graphStr = $"MATCH ({defaultLeftKey}:{labelA}), ({defaultRightKey}:{labelB}) " +
                $"WHERE {condition} " +
                $" MERGE({defaultLeftKey}) -[:{relationName}{{ relationKey: {relationKey}}}]->({defaultRightKey})";
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

        public async Task<IResultSummary> UpdateNode(string label, string condition, string setStr)
        {

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
        public async Task<List<T>> SelectNode<T>(string label, string condition) where T : class, new()
        {

            List<IRecord> records = new List<IRecord>();
            string graphStr = $"match({defaultKey}: {label}) where {condition} return {defaultKey}";
            await session.ReadTransactionAsync(async txc =>
            {
                records = await txc.RunAsync(graphStr)
                          .ContinueWith(r => r.Result.ToListAsync())
                          .Unwrap();
            });
            List<T> list = new List<T>();
            for (int i = 0; i < records.Count; i++)
            {
                var nodeList = records.Select(r => r[i].As<INode>());
                foreach (var r in nodeList)
                {
                    list.Add(JsonConvert.SerializeObject(r.Properties).ToObject<T>());
                }
            }

            return list;

        }
        /// <summary>
        /// 通过关系查询指定的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="relationShipName"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public async Task<Tuple<List<T1>, List<T2>>> SelectNodeByRelationShoip<T1, T2>(
            string leftLabel, string rightLabel, string relationShipName,string condition="", int pageCount = 10) where T1 : class, new() where T2 : class, new()
        {
            if (!string.IsNullOrWhiteSpace(condition)) {
                condition = " where " + condition;
            }
            List<IRecord> records = new List<IRecord>();
            string graphStr = $"match ({leftLabel})-[:{relationShipName}]-({rightLabel}) {condition} return {leftLabel},{rightLabel} limit {pageCount}";
            await session.ReadTransactionAsync(async txc =>
            {
                records = await txc.RunAsync(graphStr)
                          .ContinueWith(r => r.Result.ToListAsync())
                          .Unwrap();
            });
            List<T1> leftCollection = new List<T1>();
            List<T2> rigthCollection = new List<T2>();
            var temp = records.Where(x=>x.Keys.Where(t=>t.Contains(leftLabel)).Any()).Select(r => r[leftLabel].As<INode>());
            var leftNodeList = records.Select(r => r[leftLabel].As<INode>()).Where(x=>x.Labels.Contains(leftLabel)).ToList();
            var rigthNodeList = records.Select(r => r[rightLabel].As<INode>()).Where(x => x.Labels.Contains(rightLabel)).ToList(); ;
            foreach (var l in leftNodeList)
            {
                leftCollection.Add(JsonConvert.SerializeObject(l.Properties).ToObject<T1>());
            }

            foreach (var r in rigthNodeList)
            {
                rigthCollection.Add(JsonConvert.SerializeObject(r.Properties).ToObject<T2>());
            }

            return new Tuple<List<T1>, List<T2>>(leftCollection, rigthCollection);

        }


        /// <summary>
        /// 删除节点,如果deleteRelationShip 是false，但删除的有关联数据会异常，如果是关联，必须启动deleteRelationShip为true
        /// </summary>
        /// <param name="label"></param>
        /// <param name="condition"></param>
        /// <param name="defaultKey"></param>
        /// <param name="deleteRelationShip">是否联同关联都删除</param>
        /// <returns></returns>
        public async Task<IResultSummary> DeleteNode(string label, string condition, bool deleteRelationShip = false, string defaultKey = "n")
        {
            string relationDelete = "";
            if (deleteRelationShip)
            {

                relationDelete = " DETACH ";
            }
            //match(n: UserInfo{ Sex:'女' }) delete n 这种也可以
            string graphStr = $"match({defaultKey}: {label}) where {condition} {relationDelete} delete {defaultKey}";
            IResultCursor cursor = await session.RunAsync(graphStr);
            IResultSummary result = await cursor.ConsumeAsync();
            return result;
        }

        public async Task Dispose()
        {
            await session.CloseAsync();
            await driver.CloseAsync();
            GC.SuppressFinalize(this);
        }



    }

}
