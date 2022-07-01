using GraduateProject.Domain.Common;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class RouteResponseDtoV2
{
    public List<Position> Positions { get; set; } = new List<Position>();
    public List<StopDto> Stops { get; set; } = new List<StopDto>();
    
    public double Weight { get; set; }
}