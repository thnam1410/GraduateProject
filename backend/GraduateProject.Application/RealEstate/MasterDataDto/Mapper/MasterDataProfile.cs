using AutoMapper;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.MasterDataDto.Mapper;

public class MasterDataProfile: Profile
{
    public MasterDataProfile()
    {
        CreateMap<MasterData, MasterDataDetailDto>();
        CreateMap<MasterDataCreateDto, MasterData>();
        CreateMap<MasterDataUpdateDto, MasterData>();
    }
}