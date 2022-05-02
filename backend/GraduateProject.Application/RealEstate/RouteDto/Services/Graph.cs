namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class Graph: IGraph
{
    private readonly List<VertexDto> _vertices;
    private readonly List<EdgeDto> _edges;

    public Graph(List<VertexDto> vertices, List<EdgeDto> edges)
    {
        _vertices = vertices;
        _edges = edges;
    }


    public IReadOnlyCollection<VertexDto> Vertices => this._vertices.AsReadOnly();
    public IReadOnlyCollection<EdgeDto> Edges => this._edges.AsReadOnly();
    

    public EdgeDto? GetEdge(VertexDto firstVertex, VertexDto secondVertex)
    {
        return _edges.FirstOrDefault(edge => edge.PointAId == firstVertex.Id && edge.PointBId == secondVertex.Id);
    }

    public ICollection<VertexDto?> NonPermanent()
    {
        return _vertices.Where(x => x.Status != VertexStatus.Permanent).ToList();
    }

    public VertexDto? GetNonPermanentVertex()
    {
        return _vertices.FirstOrDefault(x => x.Status != VertexStatus.Permanent);
    }

    public VertexDto? GetTemporaryVertexMinCost()
    {
        double min = double.MaxValue;
        VertexDto? vertex = null;
        foreach (var currentVertex in _vertices)
        {
            if (currentVertex.Status == VertexStatus.Temporary && currentVertex.MinCost < min)
            {
                min = currentVertex.MinCost;
                vertex = currentVertex;
            }
        }
        return vertex;
    }

    public ICollection<EdgeDto> GetEdgesFromVertex(VertexDto? vertexDto)
    {
        return _edges.Where(x => x.PointAId == vertexDto.Id).ToList();
    }

    public VertexDto? GetVertexById(Guid id)
    {
        return _vertices.FirstOrDefault(x => x.Id == id);
    }
}