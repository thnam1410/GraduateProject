using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class EdgeDto
{
    public Guid PointAId { get; set; }
    public Guid PointBId { get; set; }
    public double EdgeDistance { get; set; }
    public EdgeType Type { get; set; }
}

