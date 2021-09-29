using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YC.Core;
using YC.ElasticSearch;
using YC.Model.DbEntity;

namespace YC.ElasticSearchXUnitTest
{
    public class ElasticSearchUnitTest
    {
        public IElasticSearchDbContext _elasticSearchDbContext;
        public IElasticSearchRepository<Book> _elasticSearchRepository;
        public ITenant _tenant;

        public ElasticSearchUnitTest()
        {

            string[] strArray = new string[] { "http://118.25.208.8:9200" };
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

                Id = ObjectId.Get(),
                BookName = "哈利波特",
                Auther = "J.K.罗琳",
                BookContent = " 他冲到马路对面，回到办公室，厉声吩咐秘书不要打扰他，然后抓起话筒，刚要拨通家里的电话，临时又变了卦。他放下话筒，摸着胡须，琢磨起来……不，他太愚蠢了。波特并不是一个稀有的姓，肯定有许多人姓波特，而且有儿子叫哈利。想到这里，他甚至连自己的外甥是不是叫哈利都拿不定了。他甚至没见过这孩子。说不定叫哈维，或者叫哈罗德。没有必要让太太烦心，只要一提起她妹妹，她总是心烦意乱。他并不责怪她——要是他自己有一个那样的妹妹呢……可不管怎么说，这群披斗篷的人……",
                PublishDate = DateTime.Parse("1997年6月30日"),
                Price = 57.9
            };
            var result = await _elasticSearchRepository.CreateAsync(book);
            Assert.NotNull(result);
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

                Id = ObjectId.Get(),
                BookName = "万族之劫",
                Auther = "老鹰捉小鸡",
                BookContent = "苏宇暗暗龇牙，实力不够，那就骗好了，骗万族的家伙，那是没有丝毫负罪感，最好是道成他们来买！",
                PublishDate = DateTime.Parse("2020年6月30日"),
                Price = 67.3
            });
            bookList.Add(new Book()
            {

                Id = ObjectId.Get(),
                BookName = "星门",
                Auther = "老鹰捉小鸡",
                BookContent = "李皓想了想道：“去军备库吧，也许那边会有一些收获，那边有个白银战士，就是比较牛，上次问他，爱答不理的，这次不知道会不会态度好一点。”",
                PublishDate = DateTime.Parse("2021年7月21日"),
                Price = 36
            });
            var result = await _elasticSearchRepository.CreateListAsync(bookList);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTest()
        {
            var result = await _elasticSearchRepository.MultiGetAsync();
            Assert.NotNull(result);
        }

    }
}
