using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class RouteDetailRepository: EfRepository<RouteDetail, int>, IRouteDetailRepository
{
    public RouteDetailRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}