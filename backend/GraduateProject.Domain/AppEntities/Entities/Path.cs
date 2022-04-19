using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Path: Entity<Guid>
{
    public int RouteId { get; set; }
    public Route Route { get; set; }
    public decimal Lat { get; set; }
    public decimal Lng { get; set; }
}