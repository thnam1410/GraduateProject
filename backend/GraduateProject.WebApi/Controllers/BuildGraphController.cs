using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Controllers;

public class BuildGraphController: ControllerBase
{
    private readonly IRouteDetailRepository _routeDetailRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVertexRepository _vertexRepository;
    private readonly ILogger<BuildGraphController> _logger;

    public BuildGraphController(IRouteDetailRepository routeDetailRepository, IUnitOfWork unitOfWork, IVertexRepository vertexRepository, ILogger<BuildGraphController> logger)
    {
        _routeDetailRepository = routeDetailRepository;
        _unitOfWork = unitOfWork;
        _vertexRepository = vertexRepository;
        _logger = logger;
    }


    [HttpGet("build-graph")]
    public async Task<ApiResponse> HandleBuildGraph()
    {
        var routeDetails = await _routeDetailRepository.Queryable().Include(x => x.Vertices).ToListAsync();
        var listVertex = new List<Edge>();
        foreach (var routeDetail in routeDetails)
        {
            var listPaths = routeDetail.Vertices.OrderBy(x => x.Rank).ToList();
            if (listPaths.Count() < 2) continue;
            Position startPoint = null;
            Guid pathId = default;
            for (int i = 0; i < listPaths.Count() - 1; i++)
            {
                if (startPoint is null)
                {
                    startPoint = new Position() {Lat = listPaths[i].Lat, Lng = listPaths[i].Lng};
                    pathId = listPaths[i].Id;
                }

                var pointB = listPaths[i + 1];
                var positionB = new Position() {Lat = pointB.Lat, Lng = pointB.Lng};
                var distance = CalculateUtil.Distance(startPoint, positionB);
                if (distance != 0)
                {
                    listVertex.Add(new Edge()
                    {
                        PointAId = pathId,
                        PointBId = pointB.Id,
                        Distance = distance,
                        ParentRouteDetailId = routeDetail.Id,
                        Type = EdgeType.MainRoute
                    });
                    startPoint = null;
                    pathId = default;
                }
            }
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _vertexRepository.AddEdgeList(listVertex, true);
            await _unitOfWork.CommitTransactionAsync();
            return ApiResponse.Ok();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            return ApiResponse.Fail(e.Message);
        }
    }
    
    
    
    [HttpGet("build-connect-graph")]
    public async Task<ApiResponse> HandleBuildConnectGraph()
    {
        double litmitDistance = 0.3; //300m
        var vertices = await _vertexRepository.Queryable().AsNoTracking()
            .Select(x => new EdgeSimpleDto
            {
                Id = x.Id, Lat = x.Lat, Lng = x.Lng, RouteId = x.RouteDetail.RouteId
            })
            .ToListAsync();
        var switchEdges = new List<Edge>();
        var total = vertices.Count;
        foreach (var (currentVertex, idx) in vertices.WithIndex())
        {
            _logger.LogInformation("Loop item left: " + (total - idx));
            var pointA = new Position() {Lat = currentVertex.Lat, Lng = currentVertex.Lng};
            var nearestVertex = vertices
                .Where(loopItemVertex => loopItemVertex.RouteId != currentVertex.RouteId &&
                                         CalculateUtil.Distance(pointA, new Position() {Lng = loopItemVertex.Lng, Lat = loopItemVertex.Lat}) <= litmitDistance).Distinct()
                .ToList();
            if (nearestVertex.Any())
            {
                foreach (var groupByRouteItem in nearestVertex.GroupBy(x => x.RouteId))
                {
                    var items = groupByRouteItem.ToList();
                    double min = double.MaxValue;
                    EdgeSimpleDto minItem = null;
                    foreach (var item in items)
                    {
                        var distance = CalculateUtil.Distance(pointA, new Position() {Lng = item.Lng, Lat = item.Lat});
                        if (distance > 0 && distance < min)
                        {
                            min = distance;
                            minItem = item;
                        }
                    }

                    if (minItem is not null)
                    {
                        var pointB = new Position() {Lat = minItem.Lat, Lng = minItem.Lng};
                        switchEdges.Add(new Edge()
                        {
                            PointAId = currentVertex.Id,
                            PointBId = minItem.Id,
                            Distance = CalculateUtil.Distance(pointA, pointB),
                            Type = EdgeType.SwitchRoute,
                        });
                    }
                }
            }
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _vertexRepository.BulkInsertEdgeList(switchEdges);
            await _unitOfWork.CommitTransactionAsync();
            return ApiResponse.Ok();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            return ApiResponse.Fail(e.Message);
        }
    }
    
    private class EdgeSimpleDto
    {
        public Guid Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int RouteId { get; set; }
    }
}