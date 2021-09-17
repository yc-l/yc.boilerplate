using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YC.MongoDB
{
    public class MongoDbRepository: IMongoDbRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        public MongoDbRepository(string connectionString, string dbName)
        {
            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            _client = new MongoClient(clientSettings);
            _database = _client.GetDatabase(dbName);

        }
        /// <summary>
        ///  
        /// model 如果如下，就会解析异常，要改为，主要是无类型 会乱解析[Parameter("string[]","sealerList")] 
        ///  [Parameter("sealerList")] 
        ///  public List<string> SealerList { get; set; }
        /// 
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="input"></param>
        public void Insert<T>(string collectionName, T input, FilterDefinition<T> filter = null) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);
            if (filter != null)
            {
                var data = QueryList<T>(collectionName, filter);
                if (data.Count == 0)
                    collection.InsertOne(input);
            }
            else
            {//如果启动了index 唯一索引，那么插入已经存在字段值，这里爆出异常

                collection.InsertOne(input);
            }

        }

        public void InsertList<T>(string collectionName, List<T> inputList) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);
            collection.InsertMany(inputList);
        }

        public T SingleQuery<T>(string collectionName, FilterDefinition<T> filter = null) where T : class, new()
        {
            var data = QueryList(collectionName, filter).FirstOrDefault();
            return data;
        }

        public List<T> QueryList<T>(string collectionName, FilterDefinition<T> filter = null) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);
            var projection = Builders<T>.Projection.Exclude("_id").Exclude("lastModified");
            var data = collection.Find(filter ?? new BsonDocument()).Project<T>(projection);
            return data.ToList();
        }

        public UpdateResult Update<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);
            var data = collection.UpdateMany(filter, update.CurrentDate("lastModified"));
            return data;
        }

        public DeleteResult Delete<T>(string collectionName, FilterDefinition<T> filter) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);
            var data = collection.DeleteMany(filter);
            return data;
        }

        public DeleteResult DeleteAll<T>(string collectionName) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = new BsonDocument();
            var data = collection.DeleteMany(filter);
            return data;
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="indexKeysDefinition"></param>
        /// <param name="createIndexOptions"></param>
        /// <returns></returns>
        public string CreateIndex<T>(string collectionName, IndexKeysDefinition<T> indexKeysDefinition, CreateIndexOptions createIndexOptions = null) where T : class, new()
        {
            var collection = _database.GetCollection<T>(collectionName);

            var model = new CreateIndexModel<T>(indexKeysDefinition, createIndexOptions);
            var data = collection.Indexes.CreateOne(model);
            return data;
        }

        public bool ExistUniqueIndexWithDelete<T>(string collectionName, string IndexName, bool isDelete = false) where T : class, new()
        {
            bool isExist = false;
            var collection = _database.GetCollection<T>(collectionName);
            using (var cursor = collection.Indexes.List())
            {
                var indexes = cursor.ToList();
                List<RawBsonDocument> rawBsonDocumentList = new List<RawBsonDocument>();
                indexes.ForEach(x =>
                {
                    var temp = x.Where(t => t.Name.Contains("key") && x.Names.Contains("unique"));
                    isExist = temp.Where(y => y.Value.ToString().Contains(IndexName)).Any();

                    if (isExist && isDelete)
                    {
                        var projection = Builders<T>.Projection.Exclude("_id").Exclude("lastModified");
                        collection.Indexes.DropOne(x["name"].ToString());
                    }
                });
            }

            return isExist;
        }



        /// <summary>
        /// 删除 collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public bool DropCollection<T>(string collectionName) where T : class, new()
        {
            _database.DropCollection(collectionName);
            bool state = false;
            using (var cursor = _database.ListCollectionNames())
            {
                var collections = cursor.ToList();
                state = collections.Any(x => x.Contains("collectionName")) == false ? true : false;
            }
            return state;
        }



    }
}
