namespace GraduateProject.Application.RealEstate.RouteDto;

public class RouteResponseDto
{
    public Dictionary<int, AStarPathDto> Paths { get; set; }
    public List<RouteDetailDto> RouteDetailList { get; set; }
    
}