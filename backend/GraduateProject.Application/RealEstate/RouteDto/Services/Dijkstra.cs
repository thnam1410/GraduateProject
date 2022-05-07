namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class Dijkstra
{
    // private static VertexDto _initialVertex;
    // private static VertexDto? _currentVertex;
    // private readonly IGraph _graph;
    // private static ICollection<VertexDto> _destPaths = new List<VertexDto>();
    //
    // public Dijkstra(IGraph graph, Guid startId)
    // {
    //     _graph = graph;
    //     _initialVertex = _graph.Vertices.FirstOrDefault(x => x.Id == startId)
    //                      ?? throw new Exception("Can not find path for this initial point");
    // }
    //
    // public ICollection<VertexDto> GetPaths() => _destPaths;
    //
    // public void DoDijkstra()
    // {
    //     _initialVertex.MinCost = 0;
    //     _initialVertex.SourceId = _initialVertex.Id;
    //
    //     while (true)
    //     {
    //         _currentVertex = _graph.GetTemporaryVertexMinCost();
    //
    //         if (_currentVertex is null) break;
    //
    //         _currentVertex.Status = VertexStatus.Permanent;
    //
    //         foreach (var edge in _graph.GetEdgesFromVertex(_currentVertex))
    //         {
    //             var destVertex = _graph.GetVertexById(edge.PointBId);
    //             if (destVertex is not null && destVertex.Status == VertexStatus.Temporary)
    //             {
    //                 destVertex.SourceId = _currentVertex.Id;
    //                 destVertex.MinCost = _currentVertex.MinCost + edge.EdgeDistance;
    //             }
    //         }
    //     }
    // }
    //
    // public void GeneratePathFromDestId(Guid destId)
    // {
    //     var destVertex = _graph.Vertices.FirstOrDefault(x => x.Id == destId);
    //     if (destVertex is not null)
    //     {
    //         _destPaths.Add(destVertex);
    //         if (destVertex.SourceId.HasValue)
    //         {
    //             if (destVertex.SourceId == _initialVertex.Id)
    //             {
    //                 _destPaths.Add(_initialVertex);
    //             }
    //             else
    //             {
    //                 GeneratePathFromDestId(destVertex.SourceId!.Value);
    //             }
    //         }
    //     }
    // }
}