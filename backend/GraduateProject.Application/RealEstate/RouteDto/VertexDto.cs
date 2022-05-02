namespace GraduateProject.Application.RealEstate.RouteDto;

public class VertexDto
{
    public Guid Id { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Rank { get; set; }
    
    public VertexStatus Status { get; set; }
    public double MinCost { get; set; }
    public Guid? SourceId { get; set; }
}

public enum VertexStatus
{
    Temporary,
    Permanent
}