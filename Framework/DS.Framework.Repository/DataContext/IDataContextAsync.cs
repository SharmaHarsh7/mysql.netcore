using System.Threading;
using System.Threading.Tasks;

namespace DS.Framework.Repository.Pattern.DataContext
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}   