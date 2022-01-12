namespace GraduateProject.Domain.Common;

public interface IRepository<T, TKey>
where T: IEntity<TKey>
where TKey: struct
{
    IQueryable<T> Queryable();
    Task<List<T>> ToListAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(List<T> entities);
    Task DeleteAsync(T entity);
}