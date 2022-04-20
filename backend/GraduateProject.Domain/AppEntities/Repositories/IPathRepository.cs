using GraduateProject.Domain.Common;
using Path = GraduateProject.Domain.AppEntities.Entities.Path;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IPathRepository: IRepository<Path, Guid>
{
    
}