using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Route: Entity<int>
{
    public int RouteVarId { get; set; }
    public int RouteVarName { get; set; }
    public string RouteNo { get; set; }
    public int Distance { get; set; }
    public string? EndStop { get; set; }
    public bool? Outbound { get; set; }
    public string? RouteVarShortName { get; set; }
    public int? RunningTime { get; set; }
    public string? StartStop { get; set; }

    public virtual ICollection<Path> Paths { get; set; } = new List<Path>();
    public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    public virtual ICollection<Stop> Stops { get; set; } = new List<Stop>();
}