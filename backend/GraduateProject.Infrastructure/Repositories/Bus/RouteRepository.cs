using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class RouteRepository: EfRepository<Route, int>, IRouteRepository
{
    public RouteRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}