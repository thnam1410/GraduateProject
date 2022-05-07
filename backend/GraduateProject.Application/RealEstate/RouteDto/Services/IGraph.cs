using System.Collections;
using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IGraph
{
    IReadOnlyCollection<VertexDto> Vertices { get; }
    IReadOnlyCollection<EdgeDto> Edges { get; }
    //
    //
    // ICollection<EdgeDto> GetEdgesFromVertex(VertexDto? vertexDto, EdgeType? edgeType = null);
    //
    // ICollection<VertexDto> GetNeighbors(VertexDto? vertexDto, EdgeType? edgeType = null);
    // VertexDto? GetVertexById(Guid id);
    ICollection<AStarNode> GetAStarNeighbors(AStarNode startNode, AStarNode targetNode, AStarNode currentNode, EdgeType? edgeType = null);
}