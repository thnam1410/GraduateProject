using GraduateProject.Domain.Common;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class StopDto
{
    public int Id { get; set; }
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
    public string? RouteCode { get; set; }
    public string? RouteName { get; set; }
    public double? DistanceToStart { get; set; }
    public double? DistanceToEnd { get; set; }
    
    public Position Position => new() {Lat = this.Lat, Lng = this.Lng};

}