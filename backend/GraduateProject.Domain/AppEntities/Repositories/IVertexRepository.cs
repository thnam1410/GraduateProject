using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IVertexRepository: IRepository<Vertex, Guid>
{
    IQueryable<Edge> GetEdgeQueryable();
    IQueryable<BusStopEdge> GetBusStopEdgeQueryable();

    Task AddEdgeList(List<Edge> vertices, bool autoSave = false);

    Task BulkInsertEdgeList(List<Edge> vertices);


    Task AddEdgeBusStopList(List<BusStopEdge> edges, bool autoSave = false);

}