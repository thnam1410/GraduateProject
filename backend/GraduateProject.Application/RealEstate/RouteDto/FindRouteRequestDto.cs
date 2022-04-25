using GraduateProject.Application.Common.Dto;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class FindRouteRequestDto
{
    public Position StartPoint { get; set; }
    public Position EndPoint { get; set; }
}