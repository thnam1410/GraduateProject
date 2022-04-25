using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Route: Entity<int>
{
    public string Name { get; set; }
    public List<RouteDetail> RouteDetails { get; set; }
}