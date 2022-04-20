namespace GraduateProject.Domain.Common;

public interface IRepository<T, TKey>
where T: IEntity<TKey>
where TKey: struct
{
    IQueryable<T> Queryable();
    Task<List<T>> ToListAsync();
    Task AddAsync(T entity, bool autoSave = false);
    Task AddRangeAsync(List<T> entities, bool autoSave = false);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(List<T> entities, bool autoSave = false);
    Task DeleteAsync(T entity, bool autoSave = false);
    Task DeleteRangeAsync(List<T> entites, bool autoSave = false);

    Task<T> FindByIdAsync(TKey id);
    Task<T> FindAsync(T entity);
    Task AfterInsertAsync(T entity);
    Task BeforeDeleteAsync(T entity);
    Task AfterDeleteAsync(T entity);

}