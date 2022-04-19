using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface ICrawlEntityRepository
{
    Task AddRangeCrawlRouteAsync(List<CrawlRoute> entities);
    Task AddRangeCrawlPathAsync(List<CrawlPath> entities);
    Task AddRangeCrawlStopAsync(List<CrawlStop> entities);
}