using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraduateProject.Application.Core;
using GraduateProject.Application.RealEstate.MasterDataDto;
using GraduateProject.Application.RealEstate.MasterDataDto.Services;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[Route("/api/master-data")]
[Authorize]
public class MasterDataController: BaseCrudController<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>
{   
    private readonly IMasterDataService _masterDataService;

    public MasterDataController(IMasterDataService entityService) : base(entityService)
    {
        _masterDataService = entityService;
    }

    [AllowAnonymous]
    [HttpGet("load-data")]
    public async Task<ApiResponse<List<MasterData>>> LoadMasterDataAction()
    {
        var dataResult = await _masterDataService.LoadData();
        return ApiResponse<List<MasterData>>.Ok(dataResult);
    }
}