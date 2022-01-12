using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GraduateProject.Domain.Common;

public abstract class UnitOfWork : IUnitOfWork, IDisposable
{
    public readonly DbContext _context;
    private IServiceProvider _servicesProvider;
    private IDbContextTransaction _currentTransaction;

    protected UnitOfWork(DbContext context, IServiceProvider servicesProvider)
    {
        _context = context;
        _servicesProvider = servicesProvider;
    }

    public virtual async Task<IDisposable> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        IDbContextTransaction source = await this._context.Database.BeginTransactionAsync();
        this._currentTransaction = source;
        IDisposable disposable = (IDisposable) source;
        source = (IDbContextTransaction) null;
        return disposable;
    }

    public async Task CommitTransactionAsync() => await this._currentTransaction.CommitAsync();

    public async Task RollBackTransactionAsync() => await this._currentTransaction.RollbackAsync();


    public virtual async Task SaveChangesAsync()
    {
        await this._context.SaveChangesAsync();
    }

    public void Dispose() => this._context.Dispose();
}