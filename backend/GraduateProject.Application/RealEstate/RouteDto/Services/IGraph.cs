namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IGraph
{
    IReadOnlyCollection<VertexDto> Vertices { get; }
    IReadOnlyCollection<EdgeDto> Edges { get; }
    EdgeDto? GetEdge(VertexDto firstVertex, VertexDto secondVertex);
    ICollection<VertexDto?> NonPermanent();
    VertexDto? GetNonPermanentVertex();

    VertexDto? GetTemporaryVertexMinCost();

    ICollection<EdgeDto> GetEdgesFromVertex(VertexDto? vertexDto);
    VertexDto? GetVertexById(Guid id);
}