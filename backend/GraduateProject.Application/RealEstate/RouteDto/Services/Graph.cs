using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class Graph : IGraph
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

    public ICollection<EdgeDto> GetEdgesFromVertex(VertexDto? vertexDto, EdgeType? edgeType = null)
    {
        return _edges.AsEnumerable()
            .Where(x => x.PointAId == vertexDto.Id)
            .WhereIf(edgeType.HasValue, x => x.Type == edgeType)
            .ToList();
    }

    public ICollection<VertexDto> GetNeighbors(VertexDto? vertexDto, EdgeType? edgeType = null)
    {
        var pointBIds = _edges.AsEnumerable()
            .Where(x => x.PointAId == vertexDto.Id)
            .WhereIf(edgeType.HasValue, x => x.Type == edgeType)
            .Select(x => x.PointBId)
            .ToList();
        return _vertices.Where(x => pointBIds.Contains(x.Id)).ToList();
    }

    public VertexDto? GetVertexById(Guid id)
    {
        return _vertices.FirstOrDefault(x => x.Id == id);
    }

    public ICollection<AStarNode> GetAStarNeighbors(AStarNode startNode, AStarNode targetNode, AStarNode currentNode, EdgeType? edgeType = null)
    {
        var edges = _edges.AsEnumerable()
            .Where(x => x.PointAId == currentNode.Id)
            .WhereIf(edgeType.HasValue, x => x.Type == edgeType)
            .ToList();
        var pointBIds = edges
            .Select(x => x.PointBId)
            .ToList();
        return _vertices.Where(x => pointBIds.Contains(x.Id))
            .Select(x =>
            {
                var edge = edges.First(y => y.PointBId == x.Id);
                var value = new AStarNode(x.Id, x.Lat, x.Lng, startNode, targetNode);
                if (edge.Type == EdgeType.SwitchRoute) value.IsSwitchRouteNode = true;
                return value;
            })
            .ToList();
    }
}