namespace GraduateProject.Domain.Common;

public class Position
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class PositionWithRouteInfo: Position
{
    public int RouteDetailId { get; set; }
}