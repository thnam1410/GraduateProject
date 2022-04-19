namespace GraduateProject.Domain.AppEntities.Entities;

public class RouteStop
{
    public int RouteId { get; set; }
    public Route Route { get; set; }
    public int StopId { get; set; }
    public Stop Stop { get; set; }
}