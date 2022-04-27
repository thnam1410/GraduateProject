using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Vertex
{
    
    public Guid PointAId { get; set; }
    public virtual Path PointA { get; set; }
    
    public Guid PointBId { get; set; }
    public virtual Path PointB { get; set; }
    
    public double Distance { get; set; }
    
    public int ParentRouteDetailId { get; set; }
    public virtual RouteDetail ParentRouteDetail { get; set; }
}