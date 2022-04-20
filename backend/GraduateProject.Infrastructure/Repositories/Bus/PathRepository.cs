using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Path = GraduateProject.Domain.AppEntities.Entities.Path;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class PathRepository: EfRepository<Path, Guid>, IPathRepository
{
    public PathRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}