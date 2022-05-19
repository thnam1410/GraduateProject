using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class PathHistoryRepository: EfRepository<PathHistory, Guid>, IPathHistoryRepository
{
    public PathHistoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}