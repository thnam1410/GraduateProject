using GraduateProject.Application.Common.Dto;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class VertexDto
{
    public Guid Id { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Rank { get; set; }
    public int RouteDetailId { get; set; }
    public double DistanceToStart { get; set; }
    public double DistanceToEnd { get; set; }

    public Position Position => new() {Lat = this.Lat, Lng = this.Lng};
    public PositionWithRouteInfo PositionWithRouteInfo => new() {Lat = this.Lat, Lng = this.Lng, RouteDetailId = this.RouteDetailId};
}