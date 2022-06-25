namespace GraduateProject.Domain.AppEntities.Entities;

public class BusStopEdge
{
    public int PointAId { get; set; }
    public Stop PointA { get; set; }
    
    public int PointBId { get; set; }
    public Stop PointB { get; set; }
    
    public double Distance { get; set; }
}