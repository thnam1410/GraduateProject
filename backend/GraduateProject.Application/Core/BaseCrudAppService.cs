using GraduateProject.Application.Extensions;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Application.Core;

public abstract class BaseCrudAppService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TCreateInput, TUpdateInput> : 
    ICrudAppService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
    where TCreateInput : class
    where TUpdateInput : class
{
    private readonly IUnitOfWork UnitOfWork;
    private readonly IRepository<TEntity, TKey> EntityRepository;
    private readonly IValidator _validator;
    private readonly IObjectMapper _mapper;

    protected BaseCrudAppService(IUnitOfWork unitOfWork, IRepository<TEntity, TKey> entityRepository, IObjectMapper mapper, IValidator validator)
    {
        UnitOfWork = unitOfWork;
        EntityRepository = entityRepository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public virtual async Task<PaginatedList<TGetListOutputDto>> IndexAsync(PaginatedListQuery? query = null)
    {
        IQueryable<TEntity> queryable = EntityRepository.Queryable();
        if (query is not null)
        {
            queryable = queryable.Skip(query.Offset).Take(query.Limit);
        }
        int total = await queryable.CountAsync();
        List<TEntity> entities = await queryable.Skip(query.Offset).Take(query.Limit).ToListAsync();
        List<TGetListOutputDto> items = entities.Select(x => _mapper.Map<TEntity, TGetListOutputDto>(x)).ToList();
        PaginatedList<TGetListOutputDto> paginatedList = new PaginatedList<TGetListOutputDto>(items, total, query.Offset, query.Limit);
        return paginatedList;
    }

    public virtual async Task<TGetOutputDto> CreateAsync(TCreateInput createInput)
    {
        TEntity newEntity = _validator.Validate(createInput, out var validateResult)
            ? _mapper.Map<TCreateInput, TEntity>(createInput)
            : throw new InvalidBusinessException(validateResult);
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            await EntityRepository.AddAsync(newEntity);
            await UnitOfWork.SaveChangesAsync();
            await EntityRepository.AfterInsertAsync(newEntity);
            await UnitOfWork.SaveChangesAsync();
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await UnitOfWork.RollBackTransactionAsync();
            throw;
        }        
        TGetOutputDto res = _mapper.Map<TEntity, TGetOutputDto>(newEntity);
        return res;
    }

    public virtual async Task<TGetOutputDto> GetAsync(TKey id)
    {
        TEntity entity1 = await EntityRepository.FindByIdAsync(id);
        return _mapper.Map<TEntity, TGetOutputDto>(entity1);
    }

    public virtual async Task<TGetOutputDto> UpdateAsync(TUpdateInput updateInput)
    {
        TEntity tempEntity = _validator.Validate(updateInput, out var validateResult)
            ? _mapper.Map<TUpdateInput, TEntity>(updateInput)
            : throw new InvalidBusinessException(validateResult);
        TEntity entity1 = await this.EntityRepository.FindAsync(tempEntity);
        TEntity newEntity = _mapper.Map<TUpdateInput, TEntity>(updateInput, entity1);
        await EntityRepository.UpdateAsync(newEntity);
        await UnitOfWork.SaveChangesAsync();
        TGetOutputDto result = _mapper.Map<TEntity, TGetOutputDto>(newEntity);
        return result;
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        TEntity entity = await EntityRepository.FindByIdAsync(id);
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            await this.EntityRepository.BeforeDeleteAsync(entity);
            await this.EntityRepository.DeleteAsync(entity);
            await this.UnitOfWork.SaveChangesAsync();
            await this.EntityRepository.AfterDeleteAsync(entity);
            await this.UnitOfWork.SaveChangesAsync();
            await this.UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await UnitOfWork.RollBackTransactionAsync();
            throw;
        }
    }
}