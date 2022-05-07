namespace GraduateProject.Application.RealEstate.RouteDto;

public class VertexDto
{
    public Guid Id { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Rank { get; set; }
    public int RouteDetailId { get; set; }
}