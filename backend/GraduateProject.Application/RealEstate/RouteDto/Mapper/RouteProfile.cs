using AutoMapper;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto.Mapper;

public class RouteProfile: Profile
{
    public RouteProfile()
    {
        CreateMap<Route, RouteDto>();
        CreateMap<RouteDetail, RouteDetailDto>();
        CreateMap<Stop, StopDto>();
    }
}