using GraduateProject.Domain.Common;
using Newtonsoft.Json;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlPath: Entity<Guid>
{
    public int RouteId { get; set; }
    public int RouteVarId { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
}