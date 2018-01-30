using DS.Frameowrk.Repository.Infrastructure.Pattern;
using DS.Frameowrk.Repository.Repositories.Pattern;
using System.Threading;
using System.Threading.Tasks;

namespace DS.Frameowrk.Repository.UnitOfWork.Pattern
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}