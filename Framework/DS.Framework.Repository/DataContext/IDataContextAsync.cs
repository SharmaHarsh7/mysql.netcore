using System.Threading;
using System.Threading.Tasks;

namespace DS.Framework.Repository.Pattern.DataContext
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess=true, CancellationToken cancellationToken = default(CancellationToken));
    }
}   