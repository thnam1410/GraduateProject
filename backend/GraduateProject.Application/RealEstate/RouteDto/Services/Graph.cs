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
    

    // public ICollection<EdgeDto> GetEdgesFromVertex(VertexDto? vertexDto, EdgeType? edgeType = null)
    // {
    //     return _edges.AsEnumerable()
    //         .Where(x => x.PointAId == vertexDto.Id)
    //         .WhereIf(edgeType.HasValue, x => x.Type == edgeType)
    //         .ToList();
    // }
    //
    // public ICollection<VertexDto> GetNeighbors(VertexDto? vertexDto, EdgeType? edgeType = null)
    // {
    //     var pointBIds = _edges.AsEnumerable()
    //         .Where(x => x.PointAId == vertexDto.Id)
    //         .WhereIf(edgeType.HasValue, x => x.Type == edgeType)
    //         .Select(x => x.PointBId)
    //         .ToList();
    //     return _vertices.Where(x => pointBIds.Contains(x.Id)).ToList();
    // }

    // public VertexDto? GetVertexById(Guid id)
    // {
    //     return _vertices.FirstOrDefault(x => x.Id == id);
    // }


}