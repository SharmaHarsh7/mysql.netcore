using MongoDB.Driver;

namespace DS.Framework.Mongo.Repository
{

    public interface IMongoDBContext
    {
        IMongoCollection<T> Collection<T>(MongoCollectionSettings mongoCollectionSettings = null);
        string Test();
    }
}
