using GraduateProject.Application.Core;
using GraduateProject.Application.RealEstate.MasterDataDto;
using GraduateProject.Application.RealEstate.MasterDataDto.Services;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Controllers;

public class MasterDataController: BaseCrudController<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>
{
    public MasterDataController(IMasterDataService entityService) : base(entityService)
    {
    }
}