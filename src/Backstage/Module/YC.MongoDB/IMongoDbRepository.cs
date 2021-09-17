using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.MongoDB
{
    public interface IMongoDbRepository
    {
         void Insert<T>(string collectionName, T input, FilterDefinition<T> filter = null) where T : class, new();

         void InsertList<T>(string collectionName, List<T> inputList) where T : class, new();
         T SingleQuery<T>(string collectionName, FilterDefinition<T> filter = null) where T : class, new();

         List<T> QueryList<T>(string collectionName, FilterDefinition<T> filter = null) where T : class, new();

         UpdateResult Update<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update) where T : class, new();
         DeleteResult Delete<T>(string collectionName, FilterDefinition<T> filter) where T : class, new();

         DeleteResult DeleteAll<T>(string collectionName) where T : class, new();


        string CreateIndex<T>(string collectionName, IndexKeysDefinition<T> indexKeysDefinition, CreateIndexOptions createIndexOptions = null) where T : class, new();

        bool ExistUniqueIndexWithDelete<T>(string collectionName, string IndexName, bool isDelete = false) where T : class, new();

        bool DropCollection<T>(string collectionName) where T : class, new();
    }
}
