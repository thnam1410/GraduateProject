namespace GraduateProject.Application.RealEstate.RouteDto;

public class InfoRouteSearchViewDto
{
    public RouteDto? RouteInfo { get; set; }
    public bool IsSearch { get; set; }
    public string DepartPoint { get; set; }
    public string Destination { get; set; }
    public DateTime TimeSearch { get; set; }

}