using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Route: Entity<int>
{
    public string Name { get; set; }
    public string? Type { get; set; }
    public string? BusType { get; set; }
    public string? RouteRange { get; set; }
    public string? TimeRange { get; set; }
    public string? Unit { get; set; }
    public string? RouteCode { get; set; }
    public List<RouteDetail> RouteDetails { get; set; }
}