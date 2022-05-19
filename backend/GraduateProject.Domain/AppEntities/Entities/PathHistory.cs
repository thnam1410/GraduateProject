using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class PathHistory : Entity<Guid>
{
    public double StartLat { get; set; }
    public double StartLng { get; set; }
    public double EndLat { get; set; }
    public double EndLng { get; set; }
    public string? JsonPath { get; set; }
    public bool IsError { get; set; }
    public string? ErrorMessage { get; set; }
}