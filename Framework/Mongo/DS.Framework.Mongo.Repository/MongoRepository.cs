using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Framework.Mongo.Repository
{
    public class MongoRepository<T>  : IMongoRepositoryAsync<T> where T : class
    {
        private readonly IMongoDBContext _mongoDBContext;
        public MongoRepository(IMongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public bool Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            _mongoDBContext.Collection<T>().FindOneAndDelete(filter);

            return true;
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

        public async Task<T> GetAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            return await (await _mongoDBContext.Collection<T>().FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var data = await _mongoDBContext.Collection<T>().FindAsync(x => true);

            return await data.ToListAsync();
        }

        public T Insert(T document)
        {
            _mongoDBContext.Collection<T>().InsertOne(document);

            return document;
        }

        public async Task<T> InsertAsync(T t)
        {
            await _mongoDBContext.Collection<T>().InsertOneAsync(t);

            return t;
        }

        public IMongoCollection<T> Queryable()
        {
            return _mongoDBContext.Collection<T>();
        }

    }
}
