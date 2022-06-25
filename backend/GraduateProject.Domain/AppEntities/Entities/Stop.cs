using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Stop : Entity<int>
{
    public string Name { get; set; }
    public string AddressNo { get; set; }
    public string Code { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string Routes { get; set; }
    public string Search { get; set; }
    public string Status { get; set; }
    public string StopType { get; set; }
    public string Street { get; set; }
    public int RouteVarId { get; set; }
    
    public Position Position => new() {Lat = this.Lat, Lng = this.Lng};

    public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    public virtual ICollection<RouteDetail> RouteList { get; set; } = new List<RouteDetail>();
}