namespace GraduateProject.Domain.AppEntities.Entities;

public class RouteStop
{
    public int RouteDetailId { get; set; }
    public RouteDetail RouteDetail { get; set; }
    public int StopId { get; set; }
    public Stop Stop { get; set; }
}