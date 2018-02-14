using MongoDB.Driver;
using System.Collections.Generic;

namespace DS.Framework.Mongo.Repository
{
    public interface IMongoRepository<T> where T : class
    {
        T Get(string id);
        IEnumerable<T> Get();
        T Insert(T t);

        IMongoCollection<T> Queryable();
    }
}
