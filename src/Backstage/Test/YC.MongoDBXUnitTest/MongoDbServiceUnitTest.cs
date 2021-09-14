using MongoDB.Driver;
using System;
using Xunit;
using YC.MongoDB;

namespace YC.MongoDbXUnitTest
{
    public class MongoDbServiceUnitTest
    {
        public string mongoDbConnectionString;
        public MongoDbService mongoDbService;
        public MongoDbServiceUnitTest()
        {
            mongoDbConnectionString = "mongodb://admin:123456@127.0.0.1:27017/BlockChainDb?authSource=BlockChainDb";
            mongoDbService = new MongoDbService(mongoDbConnectionString, "BlockChainDb");
           
        }

        [Fact]
        public void MongoDInsertTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var transData = mongoDbService.QueryList<TransactionsItemDto>("Transcations", transfilter);
            mongoDbService.Insert<TransactionsItemDto>("Transcations", transData[0]);
           
        }
      
        
        [Fact]
        public void MongoDbQueryTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var transData = mongoDbService.QueryList<TransactionsItemDto>("Transcations", transfilter);

            Assert.NotNull(transData);
        }

        
        [Fact]
        public void MongoDbUpdateTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var transData = mongoDbService.QueryList<TransactionsItemDto>("Transcations", transfilter);
            var updatefilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var updateOps = Builders<TransactionsItemDto>.Update.Set("Value", "abc");
            var data = mongoDbService.Update("Transcations", updatefilter, updateOps);
            Assert.Equal(4, data.ModifiedCount);
        }

        
        [Fact]
        public void MongoDbDeleteTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var data = mongoDbService.Delete("Transcations", transfilter);
            Assert.Equal(4, data.DeletedCount);
        }

       
        [Fact]
        public void MongoDbDeleteAllTest()
        {
            var data = mongoDbService.DeleteAll<BlockByNumberDto>("BlockByNumber");
            Assert.Equal(2, data.DeletedCount);
        }

     
        [Fact]
        public void MongoDbDropCollectionTest()
        {
            var state = mongoDbService.DropCollection<TransactionsItemDto>("Transcations");
            Assert.True(state);
        }

     
        [Fact]
        public void MongoDbIndexTest()
        {
            var keys = Builders<TransactionsItemDto>.IndexKeys.Ascending("Hash");
            var options = new CreateIndexOptions<TransactionsItemDto>();
            options.Unique = true;
            bool isExist = mongoDbService.ExistUniqueIndexWithDelete<TransactionsItemDto>("Transcations", "Hash");
            var data = mongoDbService.CreateIndex<TransactionsItemDto>("Transcations", keys, options);

            Assert.Equal("Hash_1", data);
        }


    }
}
