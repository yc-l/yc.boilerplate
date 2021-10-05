using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YC.Common.ShareUtils;
using YC.Core;
using YC.ElasticSearch;
using YC.Model.DbEntity;
using System.Linq;
using Nest;
using YC.ElasticSearch.Models;

namespace YC.ElasticSearchXUnitTest
{
    public class ElasticSearchUnitTest
    {
        public IElasticSearchDbContext _elasticSearchDbContext;
        public IElasticSearchRepository<Book> _elasticSearchRepository;
        public ITenant _tenant;

        public ElasticSearchUnitTest()
        {

            string[] strArray = new string[] { "http://127.0.0.1:9200" };
            _elasticSearchDbContext = new ElasticSearchDbContext(strArray);
            _tenant = new TestTenant();
            _tenant.TenantId = 1;
            //_tenant.TenantDbString = "";//可空，实际业务逻辑没有用到
            _elasticSearchRepository = new ElasticSearchRepository<Book>(_elasticSearchDbContext, _tenant);
        }

        /// <summary>
        /// 单个插入
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsyncTest()
        {
            var book = new Book()
            {

                Id = "61555adc91ef624a58fda092",
                BookName = "哈利波特",
                Auther = "J.K.罗琳",
                BookContent = " 他冲到马路对面，回到办公室，厉声吩咐秘书不要打扰他，然后抓起话筒，刚要拨通家里的电话，临时又变了卦。他放下话筒，摸着胡须，琢磨起来……不，他太愚蠢了。波特并不是一个稀有的姓，肯定有许多人姓波特，而且有儿子叫哈利。想到这里，他甚至连自己的外甥是不是叫哈利都拿不定了。他甚至没见过这孩子。说不定叫哈维，或者叫哈罗德。没有必要让太太烦心，只要一提起她妹妹，她总是心烦意乱。他并不责怪她——要是他自己有一个那样的妹妹呢……可不管怎么说，这群披斗篷的人……",
                PublishDate = DateTime.Parse("1997年6月30日"),
                Price = 57.9
            };
            string jsonData = book.ToJson();
            var result = await _elasticSearchRepository.CreateAsync(book);

            Assert.True(result.State);

        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreatListAsyncTest()
        {
            List<Book> bookList = new List<Book>();
            bookList.Add(new Book()
            {
                //Id= "615530ec3d356c46d4463e74",
                Id = ObjectId.Get(),
                BookName = "万族123",
                Auther = "老鹰捉小鸡12121",
                BookContent = "苏宇暗暗龇牙，实力不够，那就骗好了，骗万族的家伙，那是没有丝毫负罪感，最好是道成他们来买！",
                PublishDate = DateTime.Parse("2020年6月30日"),
                Price = 67.3
            });
            bookList.Add(new Book()
            {
                //Id = "615530ec3d356c46d4463e75",
                Id = ObjectId.Get(),
                BookName = "星门121",
                Auther = "老鹰捉小鸡",
                BookContent = "李皓想了想道：“去军备库吧，也许那边会有一些收获，那边有个白银战士，就是比较牛，上次问他，爱答不理的，这次不知道会不会态度好一点。”",
                PublishDate = DateTime.Parse("2021年7月21日"),
                Price = 36
            }); ; ;
            string jsonData = bookList.ToIndentedJson();
            var result = await _elasticSearchRepository.CreateListAsync(bookList,true);
            Assert.True(result.ApiCall.Success);//api 请求是否正常
            //业务操作是否成功
            var resultCount = result.Items.Where(x => x.Result == Nest.Result.Created.ToString().ToLower()).Count();
            Assert.True(resultCount == 2);
        }


        /// <summary>
        /// 批量新增,先校验是否存在
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreatListByExistValidateAsyncTest()
        {
            List<Book> bookList = new List<Book>();
            bookList.Add(new Book()
            {
                //Id= "615530ec3d356c46d4463e74",
                Id = ObjectId.Get(),
                BookName = "羊皮卷",
                Auther = "未知",
                BookContent = "这是一本书",
                PublishDate = DateTime.Parse("2020年6月30日"),
                Price = 67.3
            });
            bookList.Add(new Book()
            {
                //Id = "615530ec3d356c46d4463e75",
                Id = ObjectId.Get(),
                BookName = "中流砥柱",
                Auther = "未知",
                BookContent = "羊皮",
                PublishDate = DateTime.Parse("2021年7月21日"),
                Price = 36
            });
            string jsonData = bookList.ToIndentedJson();

            var result = await _elasticSearchRepository.CreateListByExistValidateAsync(bookList);

            Assert.True(result.Data == 2);
        }

        /// <summary>
        /// 通过_id 获取单条记录
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAsyncTest()
        {
            string id = "61555adc91ef624a58fda092";
            var result = await _elasticSearchRepository.GetAsync(id);
            Assert.NotNull(result);
        }

        /// <summary>
        /// 删除指定id数据
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteByIdTest()
        {
            string id = "61556dc18820402f3863c930";
            var result = await _elasticSearchRepository.DeleteByIdAsync(id);
            Assert.True(result.Result == Nest.Result.Deleted);
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteAllTest()
        {
            var result = await _elasticSearchRepository.DeleteAllAsync();
            Assert.True(result.Deleted > 0);
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteByQueryTest()
        {
            Func<DeleteByQueryDescriptor<Book>, IDeleteByQueryRequest> deleteOp = d =>d.Index(_elasticSearchRepository.MappingName).Query(q =>q.Range(r => r.Field(f => f.Price).GreaterThanOrEquals(200))) ;
            //查询条件
            Func<QueryContainerDescriptor<Book>, QueryContainer> query = d =>
           d.Range(r => r.Field(f => f.Price).GreaterThanOrEquals(200));
            //查询结果
            var queryList = await _elasticSearchRepository.GetByQueryAsync(query);
            //删除结果
            var result=  await _elasticSearchRepository.DeleteByQueryAsync(deleteOp);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 条件查询，多条件复合+条件范围查询
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByQueryTest()
        {
            Func<QueryContainerDescriptor<Book>, QueryContainer> query = q => q.Match(mq =>
              mq.Field(f => f.BookName).Query("万族123").Operator(Operator.And)
              )
              &&
               q.TermRange(mq =>
                mq.Field(f => f.Price).GreaterThanOrEquals("19").LessThan("100")
              )//范围查询
               ||
              q.Match(mq =>
              mq.Field(f => f.Auther).Query("老鹰捉小鸡12121").Operator(Operator.And)
              )
            ;//如果Operator.Or,那么经过分词之后其他数[万族]据也会出来

            //Func<QueryContainerDescriptor<Book>, QueryContainer> query1 = q => q.Range(mq =>
            //mq.Field(f=>f.Price).
            // );
            //排序
            Func<SortDescriptor<Book>, IPromise<IList<ISort>>> sort = s => s.Ascending(a => a.PublishDate);
            var result = await _elasticSearchRepository.GetByQueryAsync(query, sort);
            List<Book> list = result.ToList();
            Assert.True(list.Count > 0);
        }



        /// <summary>
        /// 条件查询,全字匹配
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByQueryKeyWordTest()
        {
             
            // "bookName" : {
            //"type" : "keyword"
            //},
            //BookName 修改为keyword 所有必须完整匹配，不分词
            Func<QueryContainerDescriptor<Book>, QueryContainer> query1 = q => q.Term(t => t.BookName, "万族123");
            Func<QueryContainerDescriptor<Book>, QueryContainer> query2 = q => q.Match(mq =>
              mq.Field(f => f.BookName).Query("万族").Operator(Operator.Or)
              );//由于类型为 keyword，所以Match 查找不出来，只能使用Term 精确查询
            var result = await _elasticSearchRepository.GetByQueryAsync(query1);
            List<Book> list = result.ToList();
            Assert.True(list.Count > 0);
        }

        /// <summary>
        /// 查询获取所有
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllTest()
        {
            //排序
            Func<SortDescriptor<Book>, IPromise<IList<ISort>>> sort = s => s.Ascending(a => a.PublishDate);
            var result = await _elasticSearchRepository.GetAllAsync(sort);
            List<Book> list = result.ToList();
            Assert.True(list.Count > 0);
        }

        /// <summary>
        /// 条件查询,一个关键词，多个字段匹配
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByQueryMutiTest()
        {
            string searchKey = "羊皮";
           //这里是等于平常查询一个关键词，多个字段去匹配，只要匹配到就显示
            Func<QueryContainerDescriptor<Book>, QueryContainer> query = q => q.MultiMatch(m =>m.Fields(f=>f.Fields(ff=>ff.BookName).Fields(ff => ff.BookContent))
                        .Query(searchKey));
            var result = await _elasticSearchRepository.GetByQueryAsync(query);
            List<Book> list = result.ToList();
            Assert.True(list.Count > 0);
        }

        /// <summary>
        /// 聚合查询
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByQueryAggregationsTest()
        {
            string searchKey = "羊皮";
            string avgKeyName = "BookPrice_Average";
            //1.全字匹配
            Func<QueryContainerDescriptor<Book>, QueryContainer> query1 = q => q
                           .Term(t => t.BookName, searchKey);

            //2.宽泛查询
            Func<QueryContainerDescriptor<Book>, QueryContainer> query2 = q => q
                      .Match(m=>m.Field(f=>f.BookName).Query(searchKey));
            Func<AggregationContainerDescriptor<Book>, IAggregationContainer> aggs1=a=>
               a.Average(avgKeyName, aa => aa.Field(f => f.Price));//统计平均
            Func<AggregationContainerDescriptor<Book>, IAggregationContainer> aggs2 = a =>
                a.ExtendedStats(avgKeyName, aa => aa.Field(f => f.Price));//统计所有
            Func<AggregationContainerDescriptor<Book>, IAggregationContainer> aggs3 = a =>
              a.Max(avgKeyName, aa => aa.Field(f => f.Price));//最大
            Func<AggregationContainerDescriptor<Book>, IAggregationContainer> aggs4 = a =>
              a.Percentiles(avgKeyName, aa => aa.Field(f => f.Price));//百分数

           

            var result = await _elasticSearchRepository.GetByQueryAggregationsAsync(query2, aggs3);
            List<Book> list = result.Item1.ToList();
            var aggResult = result.Item2[avgKeyName];
            //AggregateDictionary;
            var data=(ExtendedStatsAggregate)aggResult;//这里要根据返回的结果转化指定的类型，通过AggregateDictionary查找对应的类型
            var valueAggregate =((ValueAggregate)aggResult).Value;//获取得到指定的平局数
            Assert.True(list.Count > 0);
        }

        /// <summary>
        /// 通过id 更新
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateTest()
        {
            string id = "6155a94eaa40676904f987b9";
            Func<QueryContainerDescriptor<Book>, QueryContainer> query = q => q.Match(mq =>
             mq.Field(f => f.Id).Query(id).Operator(Operator.And)
             );
            var queryList = await _elasticSearchRepository.GetByQueryAsync(query);
            var obj = queryList.FirstOrDefault();
            obj.Price = 200;
            var result = await _elasticSearchRepository.UpdateAsync(id, obj);
            Assert.NotNull(result);
        }



        [Fact]
        public async Task AllTest()
        {

           var result = await _elasticSearchDbContext.Client.UpdateByQueryAsync<Book>(s => s
                              .Index(_elasticSearchRepository.MappingName) //or specify index via 
                             .Query(q => q.Match(mq => mq.Field(f => f.BookName).Query("万族123").Operator(Operator.And))));

        }




    }
}
