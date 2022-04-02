using GraduateProject.Application.Core;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Application.RealEstate.MasterDataDto.Services;

public class MasterDataService: BaseCrudAppService<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>, IMasterDataService
{
    
    private readonly IMasterDataRepository _masterDataRepository;

    public MasterDataService(IUnitOfWork unitOfWork, IMasterDataRepository entityRepository, IObjectMapper mapper, IValidator validator) : base(unitOfWork, entityRepository, mapper, validator)
    {
        _masterDataRepository = entityRepository;
    }
    public async Task<List<MasterData>> LoadData()
    {
        var masterData = await _masterDataRepository.ToListAsync();
        return masterData;
    }
}