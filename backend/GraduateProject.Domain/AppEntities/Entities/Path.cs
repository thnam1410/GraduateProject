using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Path: Entity<Guid>
{
    public int RouteDetailId { get; set; }
    public RouteDetail RouteDetail { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
}