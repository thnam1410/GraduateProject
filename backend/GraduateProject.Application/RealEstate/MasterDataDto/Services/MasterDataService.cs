using GraduateProject.Application.Core;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Application.RealEstate.MasterDataDto.Services;

public class MasterDataService: BaseCrudAppService<MasterData, Guid, MasterDataDetailDto, MasterDataDetailDto, MasterDataCreateDto, MasterDataUpdateDto>, IMasterDataService
{
    
    private readonly IMasterDataRepository _masterDataRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IObjectMapper _mapper;
    public MasterDataService(IUnitOfWork unitOfWork, IMasterDataRepository entityRepository, IObjectMapper mapper, IValidator validator) : base(unitOfWork, entityRepository, mapper, validator)
    {
        _unitOfWork = unitOfWork;
        _masterDataRepository = entityRepository;
        _mapper = mapper;
    }
    public async Task<List<MasterData>> LoadData()
    {
        var masterData = await _masterDataRepository.ToListAsync();
        return masterData;
    }
    
    // public async Task<ApiResponse<object>> CreateAsync(MasterDataCreateDto masterDataForm)
    // {
    //     var masterData = new MasterData()
    //     {
    //         Name = masterDataForm.Name,
    //         Code = masterDataForm.Code,
    //         MasterKey = masterDataForm.MasterKey
    //     };
    //     try
    //     {
    //         await _unitOfWork.BeginTransactionAsync();
    //         await _masterDataRepository.AddAsync(masterData);
    //         await _unitOfWork.SaveChangesAsync();
    //         await _unitOfWork.CommitTransactionAsync();
    //         return ApiResponse<object>.Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         await _unitOfWork.RollBackTransactionAsync();
    //         return ApiResponse<object>.Fail(e.Message);
    //     }
    // }
}