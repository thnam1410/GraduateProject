using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraduateProject.Application.Core;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Extensions;

namespace GraduateProject.Application.RealEstate.MasterDataDto.Services;

public interface IMasterDataService: ICrudAppService<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>
{
    Task<List<MasterData>> LoadData();
    // Task<ApiResponse<object>> CreateAsync(MasterDataCreateDto masterDataForm);
}