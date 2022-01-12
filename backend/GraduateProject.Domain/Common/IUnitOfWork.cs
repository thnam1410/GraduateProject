using System.Data;

namespace GraduateProject.Domain.Common;

public interface IUnitOfWork
{
    Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
    Task SaveChangesAsync();

}