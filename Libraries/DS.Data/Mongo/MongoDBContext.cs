using DS.Core.Configuration;
using DS.Framework.Mongo.Repository;
using MongoDB.Driver;

namespace DS.Data.Mongo
{
    public class MongoDBContext : IMongoDBContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDBContext(MongoConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<T> Collection<T>(MongoCollectionSettings mongoCollectionSettings = null)
        {
            return _database.GetCollection<T>(typeof(T).Name, mongoCollectionSettings);
        }

        public string Test()
        {
            return "ok";
        }
    }
}
