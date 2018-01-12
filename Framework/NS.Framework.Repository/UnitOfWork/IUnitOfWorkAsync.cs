using NS.Frameowrk.Repository.Infrastructure.Pattern;
using NS.Frameowrk.Repository.Repositories.Pattern;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Frameowrk.Repository.UnitOfWork.Pattern
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}