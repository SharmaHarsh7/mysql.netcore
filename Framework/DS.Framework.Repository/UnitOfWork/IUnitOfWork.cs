using DS.Frameowrk.Repository.Infrastructure.Pattern;
using DS.Frameowrk.Repository.Repositories.Pattern;
using System;
using System.Data;

namespace DS.Frameowrk.Repository.UnitOfWork.Pattern
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}