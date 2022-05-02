using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class RouteDetail: Entity<int>
{
    public int RouteVarId { get; set; }
    public string RouteVarName { get; set; }
    public string RouteNo { get; set; }
    public double Distance { get; set; }
    public string? EndStop { get; set; }
    public bool? Outbound { get; set; }
    public string? RouteVarShortName { get; set; }
    public int? RunningTime { get; set; }
    public string? StartStop { get; set; }
    public int RouteId { get; set; }
    public Route Route { get; set; }

    public virtual ICollection<Vertex> Paths { get; set; } = new List<Vertex>();
    public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    public virtual ICollection<Stop> Stops { get; set; } = new List<Stop>();
}