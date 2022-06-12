using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface ICrawlEntityRepository
{
    Task AddRangeCrawlRouteDetailAsync(List<CrawlRouteDetail> entities);
    Task AddRangeCrawlPathAsync(List<CrawlPath> entities);
    Task AddRangeCrawlStopAsync(List<CrawlStop> entities);

    Task AddRangeCrawlRouteAsync(List<CrawlRoute> entities);

    IQueryable<T> GenericQueryable<T>() where T: class;
}