using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Common;

public class FileRepository: EfRepository<FileEntry, Guid>, IFileRepository
{
    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}