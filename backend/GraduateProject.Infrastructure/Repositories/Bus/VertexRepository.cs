using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class VertexRepository: EfRepository<Vertex, Guid>, IVertexRepository
{
    public VertexRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Edge> GetEdgeQueryable()
    {
        return _dbContext.Set<Edge>().AsQueryable();
    }
    
    public IQueryable<BusStopEdge> GetBusStopEdgeQueryable()
    {
        return _dbContext.Set<BusStopEdge>().AsQueryable();
    }

    public async Task AddEdgeList(List<Edge> vertices, bool autoSave = false)
    {
        await _dbContext.Set<Edge>().AddRangeAsync(vertices);
        if (autoSave) await _dbContext.SaveChangesAsync();
    }

    public async Task AddEdgeBusStopList(List<BusStopEdge> edges, bool autoSave = false)
    {
        await _dbContext.Set<BusStopEdge>().AddRangeAsync(edges);
        if (autoSave) await _dbContext.SaveChangesAsync();
    }
    
    public async Task BulkInsertEdgeList(List<Edge> vertices)
    {
        await _dbContext.Set<Edge>().AddRangeAsync(vertices);
        await _dbContext.SaveChangesAsync();
    }
    
}