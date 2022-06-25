using GraduateProject.Application.Common.Dto;
using GraduateProject.Domain.Common;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class FindRouteRequestDto
{
    public Position StartPoint { get; set; }
    public Position EndPoint { get; set; }
}