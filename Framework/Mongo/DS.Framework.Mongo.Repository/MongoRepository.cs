using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Framework.Mongo.Repository
{
    public class MongoRepository<T>  : IMongoRepository<T> where T : class
    {
        private readonly IMongoDBContext _mongoDBContext;
        public MongoRepository(IMongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }


        public T Get(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            return _mongoDBContext.Collection<T>().Find(filter).FirstOrDefault();
        }

        public IEnumerable<T> Get()
        {
            return _mongoDBContext.Collection<T>().Find(x => true).ToList();
        }



        public T Insert(T document)
        {
            _mongoDBContext.Collection<T>().InsertOne(document);

            return document;
        }

        public IMongoCollection<T> Queryable()
        {
            return _mongoDBContext.Collection<T>();
        }

    }
}
