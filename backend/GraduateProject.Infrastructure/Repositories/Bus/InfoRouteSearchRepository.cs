using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class InfoRouteSearchRepository: EfRepository<InfoRouteSearch, int>, IInfoRouteSearchRepository
{
    public InfoRouteSearchRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}