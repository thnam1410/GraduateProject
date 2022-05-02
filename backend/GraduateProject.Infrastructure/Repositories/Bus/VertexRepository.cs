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

    public async Task AddEdgeList(List<Edge> vertices, bool autoSave = false)
    {
        await _dbContext.Set<Edge>().AddRangeAsync(vertices);
        if (autoSave) await _dbContext.SaveChangesAsync();
    }    
    public async Task BulkInsertEdgeList(List<Edge> vertices)
    {
        await _dbContext.Set<Edge>().BulkInsertAsync(vertices, opt => opt.AutoMapOutputDirection = false);
        await _dbContext.BulkSaveChangesAsync(x => x.BatchSize = 1000);
    }
    
}