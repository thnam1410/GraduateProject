using GraduateProject.Domain.Common;

namespace GraduateProject.Application.Core;

public interface ICrudAppService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TCreateInput, TUpdateInput>
    where TEntity : IEntity<TKey>
{
    Task<PaginatedList<TGetListOutputDto>> IndexAsync(PaginatedListQuery? query = null);

    Task<TGetOutputDto> CreateAsync(TCreateInput createInput);

    Task<TGetOutputDto> GetAsync(TKey id);

    Task<TGetOutputDto> UpdateAsync(TUpdateInput createInput);

    Task DeleteAsync(TKey id);
}
