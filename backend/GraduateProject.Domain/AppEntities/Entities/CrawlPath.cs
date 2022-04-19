using GraduateProject.Domain.Common;
using Newtonsoft.Json;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlPath: Entity<Guid>
{
    public int RouteId { get; set; }
    public decimal Lat { get; set; }
    public decimal Lng { get; set; }
}