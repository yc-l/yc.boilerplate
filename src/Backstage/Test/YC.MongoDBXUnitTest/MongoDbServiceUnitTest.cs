using MongoDB.Driver;
using System;
using Xunit;
using YC.MongoDB;

namespace YC.MongoDbXUnitTest
{
    public class MongoDbServiceUnitTest
    {
        public string mongoDbConnectionString;
        public MongoDbRepository mongoDbRepository;
        public MongoDbServiceUnitTest()
        {
            mongoDbConnectionString = "mongodb://admin:123456@127.0.0.1:27017/BlockChainDb?authSource=BlockChainDb";
            mongoDbRepository = new MongoDbRepository(mongoDbConnectionString, "BlockChainDb");
           
        }

        [Fact]
        public void MongoDInsertTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var transData = mongoDbRepository.QueryList<TransactionsItemDto>("Transcations", transfilter);
            mongoDbRepository.Insert<TransactionsItemDto>("Transcations", transData[0]);
           
        }
      
        
        [Fact]
        public void MongoDbQueryTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var transData = mongoDbRepository.QueryList<TransactionsItemDto>("Transcations", transfilter);

            Assert.NotNull(transData);
        }

        
        [Fact]
        public void MongoDbUpdateTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var transData = mongoDbRepository.QueryList<TransactionsItemDto>("Transcations", transfilter);
            var updatefilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var updateOps = Builders<TransactionsItemDto>.Update.Set("Value", "abc");
            var data = mongoDbRepository.Update("Transcations", updatefilter, updateOps);
            Assert.Equal(4, data.ModifiedCount);
        }

        
        [Fact]
        public void MongoDbDeleteTest()
        {
            var transfilter = Builders<TransactionsItemDto>.Filter.Where(x => x.Signature.V.Contains("0x0"));
            var data = mongoDbRepository.Delete("Transcations", transfilter);
            Assert.Equal(4, data.DeletedCount);
        }

       
        [Fact]
        public void MongoDbDeleteAllTest()
        {
            var data = mongoDbRepository.DeleteAll<BlockByNumberDto>("BlockByNumber");
            Assert.Equal(2, data.DeletedCount);
        }

     
        [Fact]
        public void MongoDbDropCollectionTest()
        {
            var state = mongoDbRepository.DropCollection<TransactionsItemDto>("Transcations");
            Assert.True(state);
        }

     
        [Fact]
        public void MongoDbIndexTest()
        {
            var keys = Builders<TransactionsItemDto>.IndexKeys.Ascending("Hash");
            var options = new CreateIndexOptions<TransactionsItemDto>();
            options.Unique = true;
            bool isExist = mongoDbRepository.ExistUniqueIndexWithDelete<TransactionsItemDto>("Transcations", "Hash");
            var data = mongoDbRepository.CreateIndex<TransactionsItemDto>("Transcations", keys, options);

            Assert.Equal("Hash_1", data);
        }


    }
}
