using GraduateProject.Application.Extensions;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Application.Core;

public abstract class BaseCrudController<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TCreateInput, TUpdateInput> : ControllerBase
    where TEntity : class, IEntity<TKey>
    where TKey : struct
    where TGetOutputDto : class
    where TGetListOutputDto : class
{
    private ICrudAppService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TCreateInput, TUpdateInput> _entityService;

    protected BaseCrudController(ICrudAppService<TEntity, TKey, TGetOutputDto, TGetListOutputDto, TCreateInput, TUpdateInput> entityService)
    {
        _entityService = entityService;
    }

    [HttpGet("index")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ApiResponse<PaginatedList<TGetListOutputDto>>> HandleIndexAction()
    {
        PaginatedList<TGetListOutputDto> result = await this._entityService.IndexAsync(this.Request.Query.GetPaginatedListQuery());
        ApiResponse<PaginatedList<TGetListOutputDto>> apiResponse = ApiResponse<PaginatedList<TGetListOutputDto>>.Ok(result);
        return apiResponse;
    }

    [HttpGet("show/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public virtual async Task<ApiResponse<TGetOutputDto>> ShowAction(TKey id)
    {
        TGetOutputDto getOutputDto = await this._entityService.GetAsync(id);
        TGetOutputDto result = getOutputDto;
        ApiResponse<TGetOutputDto> apiResponse = ApiResponse<TGetOutputDto>.Ok(result);
        return apiResponse;
    }

    [HttpPost("create")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ApiResponse<TGetOutputDto>> HandleCreateAction(
        [FromBody] TCreateInput request)
    {
        TGetOutputDto getOutputDto = await this._entityService.CreateAsync(request);
        TGetOutputDto result = getOutputDto;
        ApiResponse<TGetOutputDto> action = ApiResponse<TGetOutputDto>.Ok(result);
        return action;
    }

    [HttpPost("update")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ApiResponse<TGetOutputDto>> HandleUpdateAction(
        [FromBody] TUpdateInput request)
    {
        TGetOutputDto getOutputDto = await this._entityService.UpdateAsync(request);
        TGetOutputDto result = getOutputDto;
        ApiResponse<TGetOutputDto> apiResponse = ApiResponse<TGetOutputDto>.Ok(result);
        return apiResponse;
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ApiResponse<object>> HandleDeleteAction(TKey id)
    {
        await this._entityService.DeleteAsync(id);
        return ApiResponse<object>.Ok((object) null, (string) null);
    }
}