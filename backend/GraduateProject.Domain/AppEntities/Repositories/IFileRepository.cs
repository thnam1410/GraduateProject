using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IFileRepository: IRepository<FileEntry, Guid>
{
    
}