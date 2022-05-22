using GraduateProject.Application.RealEstate.RouteDto;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace GraduateProject.Extensions;

public static class PreCaching
{
    public static void PreCacheVerticesAndPath(this IApplicationBuilder app)
    {
        var executeScope = app.ApplicationServices.CreateScope();
        var vertexRepo = executeScope.ServiceProvider.GetRequiredService<IVertexRepository>();
        vertexRepo.Queryable().AsNoTracking()
            .Select(x => new VertexDto()
            {
                Id = x.Id,
                Lat = x.Lat,
                Lng = x.Lng,
                Rank = x.Rank,
                RouteDetailId = x.RouteDetailId
            })
            .FromCache();
        vertexRepo.GetEdgeQueryable().AsNoTracking()
            .Select(x => new EdgeDto()
            {
                PointAId = x.PointAId,
                PointBId = x.PointBId,
                EdgeDistance = x.Distance,
                Type = x.Type
            }).FromCache();
    }
}