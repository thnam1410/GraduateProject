using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Domain.Common;

public class EfRepository<T, TKey> : IRepository<T, TKey>
    where T : class, IEntity<TKey>
    where TKey : struct
{
    protected DbContext _dbContext;
    protected DbSet<T> _dbSet;

    public EfRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public virtual IQueryable<T> Queryable() => (IQueryable<T>) this._dbSet;

    public virtual async Task<List<T>> ToListAsync() => (List<T>) await this._dbSet.ToListAsync<T>();

    public virtual async Task AddAsync(T entity)
    {
        await this._dbSet.AddAsync(entity);
    }

    public virtual Task AddRangeAsync(List<T> entities) => this._dbContext.AddRangeAsync((IEnumerable<object>) entities);


    public virtual async Task UpdateAsync(T entity)
    {
        await Task.CompletedTask;
        this._dbContext.Update<T>(entity);
    }

    public virtual Task UpdateRangeAsync(List<T> entities)
    {
        this._dbContext.UpdateRange((IEnumerable<object>) entities);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        await Task.CompletedTask;
        this._dbSet.Remove(entity);
    }
}