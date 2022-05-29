namespace GraduateProject.Application.RealEstate.RouteDto;

public class RouteResponseDto
{
    public Dictionary<int, AStarPathDto> Paths { get; set; } = new Dictionary<int, AStarPathDto>();
    public List<RouteDto> RouteDetailList { get; set; } = new List<RouteDto>();
    public List<StopDto> Stops { get; set; } = new List<StopDto>();

}