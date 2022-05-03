namespace GraduateProject.Application.RealEstate.RouteDto;

public class RouteDetailDto
{
    public string RouteVarName { get; set; }
    public string RouteNo { get; set; }
    public double Distance { get; set; }
    public string? EndStop { get; set; }
    public bool? Outbound { get; set; }
    public string? RouteVarShortName { get; set; }
    public int? RunningTime { get; set; }
    public string? StartStop { get; set; }
    public int RouteId { get; set; }
}