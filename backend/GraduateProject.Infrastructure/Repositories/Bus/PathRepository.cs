﻿using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Path = GraduateProject.Domain.AppEntities.Entities.Path;

namespace GraduateProject.Infrastructure.Repositories.Bus;

public class PathRepository: EfRepository<Path, Guid>, IPathRepository
{
    public PathRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Vertex> GetVertexQueryable()
    {
        return _dbContext.Set<Vertex>().AsQueryable();
    }

    public async Task AddVertexList(List<Vertex> vertices, bool autoSave = false)
    {
        await _dbContext.Set<Vertex>().AddRangeAsync(vertices);
        if (autoSave) await _dbContext.SaveChangesAsync();
    }
    
}