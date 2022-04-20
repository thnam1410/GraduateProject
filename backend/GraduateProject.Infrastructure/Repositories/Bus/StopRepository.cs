using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class StopRepository: EfRepository<Stop, int>, IStopRepository
{
    public StopRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}