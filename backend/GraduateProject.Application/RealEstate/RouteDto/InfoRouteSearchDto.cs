using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class InfoRouteSearchDto
{
    public Guid? UserId { get; set; }
    public int? RouteId { get; set; }
    public bool? IsSearch { get; set; }
    public string? DepartPoint { get; set; }
    public string? Destination { get; set; }
    public string? TimeSearchDto { get; set; }
    public Route? Route { get; set; }

}