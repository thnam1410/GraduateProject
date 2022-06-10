using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class InfoRouteSearch : Entity<int>
{
    public Guid? UserId { get; set; }
    public int? RouteId { get; set; }
    public bool? IsSearch { get; set; }
    public string? DepartPoint { get; set; }
    public string? Destination { get; set; }
    public DateTime? TimeSearch { get; set; }
    public Route? Route { get; set; }

}