using AutoMapper;
using GraduateProject.Domain.Ums.Entities;

namespace GraduateProject.Application.Ums.Dto.Mapper;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserAccount, UserInfoDto>();
    }
}