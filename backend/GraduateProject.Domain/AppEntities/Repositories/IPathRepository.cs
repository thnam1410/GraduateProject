using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;
using Path = GraduateProject.Domain.AppEntities.Entities.Path;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IPathRepository: IRepository<Path, Guid>
{
    IQueryable<Vertex> GetVertexQueryable();

    Task AddVertexList(List<Vertex> vertices, bool autoSave = false);
}