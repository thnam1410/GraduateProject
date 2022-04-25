using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class RouteRepository : EfRepository<Route, int>, IRouteRepository
{
    public RouteRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task UpdateIdentityInsert(bool isOn)
    {
        var entityType = _dbContext.Model.FindEntityType(typeof(Route));
        var mode = isOn ? "ON" : "OFF";
        await _dbContext.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {entityType!.GetSchema()}.{entityType!.GetTableName()} {mode};");
    }
}