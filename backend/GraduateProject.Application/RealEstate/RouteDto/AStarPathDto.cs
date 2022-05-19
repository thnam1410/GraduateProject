using GraduateProject.Application.Common.Dto;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class AStarPathDto
{
    public bool IsSwitch { get; set; }
    public List<Position> Positions { get; set; }
}