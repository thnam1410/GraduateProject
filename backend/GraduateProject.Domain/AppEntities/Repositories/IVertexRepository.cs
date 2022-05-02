using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IVertexRepository: IRepository<Vertex, Guid>
{
    IQueryable<Edge> GetEdgeQueryable();

    Task AddVertexList(List<Edge> vertices, bool autoSave = false);
}