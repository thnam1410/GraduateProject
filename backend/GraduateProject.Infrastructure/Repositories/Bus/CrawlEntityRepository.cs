using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class CrawlEntityRepository: ICrawlEntityRepository
{
    private readonly AppDbContext _dbContext;

    public CrawlEntityRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeCrawlRouteDetailAsync(List<CrawlRouteDetail> entities)
    {
        await _dbContext.Set<CrawlRouteDetail>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }
    public async Task AddRangeCrawlPathAsync(List<CrawlPath> entities)
    {
        await _dbContext.Set<CrawlPath>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }
    public async Task AddRangeCrawlStopAsync(List<CrawlStop> entities)
    {
        await _dbContext.Set<CrawlStop>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeCrawlRouteAsync(List<CrawlRoute> entities)
    {
        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.CrawlRoute ON;");
        await _dbContext.Set<CrawlRoute>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<T> GenericQueryable<T>() where T : class
    {
        return _dbContext.Set<T>().AsQueryable();
    }
}