using GraduateProject.Application.Core;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.MasterDataDto.Services;

public interface IMasterDataService: ICrudAppService<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>
{
    
}