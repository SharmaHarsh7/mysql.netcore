using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Framework.Mongo.Repository
{
    public interface IMongoRepositoryAsync<T> : IMongoRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAsync();
        Task<T> InsertAsync(T t);

    }
}
