using GraduateProject.Application.Core;
using GraduateProject.Application.RealEstate.MasterDataDto;
using GraduateProject.Application.RealEstate.MasterDataDto.Services;
using GraduateProject.Domain.AppEntities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[Route("/api/master-data")]
[Authorize]
public class MasterDataController: BaseCrudController<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>
{
    public MasterDataController(IMasterDataService entityService) : base(entityService)
    {
    }
}