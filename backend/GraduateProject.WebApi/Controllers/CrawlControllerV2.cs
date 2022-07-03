using GraduateProject.Application.Extensions;
using GraduateProject.Common.Dto;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route = GraduateProject.Domain.AppEntities.Entities.Route;

namespace GraduateProject.Controllers;

public class CrawlControllerV2 : ControllerBase
{
    private readonly ILogger<CrawlerController> _logger;
    private readonly IObjectMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICrawlEntityRepository _crawlEntityRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IVertexRepository _vertexRepository;
    private readonly IRouteDetailRepository _routeDetailRepository;

    public CrawlControllerV2(ILogger<CrawlerController> logger, IObjectMapper mapper, IUnitOfWork unitOfWork, ICrawlEntityRepository crawlEntityRepository,
        IStopRepository stopRepository, IRouteRepository routeRepository, IVertexRepository vertexRepository, IRouteDetailRepository routeDetailRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _crawlEntityRepository = crawlEntityRepository;
        _stopRepository = stopRepository;
        _routeRepository = routeRepository;
        _vertexRepository = vertexRepository;
        _routeDetailRepository = routeDetailRepository;
    }

    [HttpGet("start")]
    public async Task<ApiResponse> HandleCrawlerRoutes()
    {
        HttpClient client = new HttpClient();
        var listRouteId = await ListRouteId(client);

        var listRouteDetails = new List<CrawlRouteDetail>();
        var listStops = new List<CrawlStop>();
        var listPath = new List<CrawlPathDto>();
        var listRoutes = new List<CrawlRoute>();
        foreach (var routeId in listRouteId)
        {
            var resRouteInfo = await client.GetAsync($"http://apicms.ebms.vn/businfo/getroutebyid/{routeId}");
            var resultRouteInfo = await resRouteInfo.Content.ReadFromJsonAsync<CrawlRoute>();
            if (resultRouteInfo is not null) listRoutes.Add(resultRouteInfo);

            var resUrl = await client.GetAsync($"http://apicms.ebms.vn/businfo/getvarsbyroute/{routeId}");
            var resultRouteDetail = await resUrl.Content.ReadFromJsonAsync<List<CrawlRouteDetail>>();
            if (resultRouteDetail is not null && resultRouteDetail.Any())
            {
                listRouteDetails.AddRange(resultRouteDetail);
                foreach (var routeVarId in resultRouteDetail.Select(x => x.RouteVarId))
                {
                    try
                    {
                        var stopUrl = $"http://apicms.ebms.vn/businfo/getstopsbyvar/{routeId}/{routeVarId}";
                        _logger.LogInformation($"Fetching API: {stopUrl}");
                        var responseStopData = await client.GetAsync(stopUrl);
                        responseStopData.EnsureSuccessStatusCode();

                        var pathUrl = $"http://apicms.ebms.vn/businfo/getpathsbyvar/{routeId}/{routeVarId}";
                        _logger.LogInformation($"Fetching API: {pathUrl}");
                        var responsePathData = await client.GetAsync(pathUrl);
                        responsePathData.EnsureSuccessStatusCode();

                        var stopData = await responseStopData.Content.ReadFromJsonAsync<List<CrawlStop>>();
                        var pathData = await responsePathData.Content.ReadFromJsonAsync<CrawlPathDto>();

                        if (stopData is not null && stopData.Any())
                        {
                            foreach (var (stop, idx) in stopData.WithIndex())
                            {
                                stop.RouteVarId = routeVarId;
                                stop.RouteId = routeId;
                                stop.Rank = idx + 1;
                            }
                            listStops.AddRange(stopData);
                        }
                        
                        if (pathData is not null)
                        {
                            pathData.RouteId = routeId;
                            pathData.RouteVarId = routeVarId;
                            listPath.Add(pathData);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation($"Fetch api fail at routeVarId: {routeVarId} - routeId: {routeId}");
                    }
                }
            }
        }

        var listCrawlEntityRoutes = listRoutes;
        var listCrawlEntityRouteDetail = listRouteDetails;
        var listCrawlEntityStops = listStops;
        var listCrawlEntityPath = new List<CrawlPath>();
        foreach (var path in listPath)
        {
            for (int i = 0; i < path.Lat.Count(); i++)
            {
                listCrawlEntityPath.Add(new CrawlPath()
                {
                    RouteId = path.RouteId.Value,
                    RouteVarId = path.RouteVarId.Value,
                    Lat = path.Lat[i],
                    Lng = path.Lng[i],
                    Rank = i + 1
                });
            }
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _crawlEntityRepository.AddRangeCrawlRouteAsync(listCrawlEntityRoutes);
            _logger.LogInformation("Crawl Routes into DB successfully!");
            
            await _crawlEntityRepository.AddRangeCrawlRouteDetailAsync(listCrawlEntityRouteDetail);
            _logger.LogInformation("Crawl Routes Detail into DB successfully!");
            
            await _crawlEntityRepository.AddRangeCrawlPathAsync(listCrawlEntityPath);
            _logger.LogInformation("Crawl Path into DB successfully!");

            await _crawlEntityRepository.AddRangeCrawlStopAsync(listCrawlEntityStops);
            _logger.LogInformation("Crawl Stops into DB successfully!");

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            return ApiResponse.Fail(e.Message);
        }

        return ApiResponse.Ok();
    }


    [HttpGet("trigger-preprocess-data-stop-1")]
    public async Task<ApiResponse> HandlePreprocessDataStop()
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            List<Stop> stopEntities = await PreprocessStopEntities();
            await _stopRepository.AddRangeAsync(stopEntities, true);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
        }

        return ApiResponse.Ok();
    }

    [HttpGet("trigger-preprocess-data-route-2")]
    public async Task<ApiResponse> HandlePreprocessDataRoute()
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            List<Route> routeEntities = await PreprocessRouteEntities();
            await _routeRepository.UpdateIdentityInsert(true);
            await _routeRepository.AddRangeAsync(routeEntities, true);
            await _routeRepository.UpdateIdentityInsert(false);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            return ApiResponse.Fail(e.Message);
        }

        return ApiResponse.Ok();
    }
    
    [HttpGet("trigger-preprocess-data-path-3")]
    public async Task<ApiResponse> HandlePreprocessDataPath()
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            List<Vertex> pathEntities = await PreprocessPathEntities();
            await _vertexRepository.AddRangeAsync(pathEntities, true);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
        }

        return ApiResponse.Ok();
    }
    
    [HttpGet("trigger-preprocess-data-stop-route-4")]
    public async Task<ApiResponse> HandlePreprocessStopRoute()
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var routeDetailEntities = await _routeDetailRepository.Queryable()
                .Include(x => x.Route).ToListAsync();
            List<Stop> stopEntities = await _stopRepository.Queryable().ToListAsync();
            foreach (var routeDetail in routeDetailEntities)
            {
                var routeStops = stopEntities.Where(x => x.RouteId == routeDetail.RouteId && x.RouteVarId == routeDetail.RouteVarId).ToList();
                routeDetail.Stops = routeStops;
            }
            // foreach (var stop in stopEntities)
            // {
            //     var listRouteCode = stop.Routes.Split(",").Select(x => x.Trim());
            //     foreach (var routeCode in listRouteCode)
            //     {
            //         var routeDetail = routeDetailEntities.FirstOrDefault(x => string.Equals(x.Route.RouteCode, routeCode, StringComparison.CurrentCultureIgnoreCase) && x.RouteVarId == stop.RouteVarId);
            //         if (routeDetail is not null && routeDetail.RouteStops.All(x => x.StopId != stop.Id && x.RouteDetailId != routeDetail.Id))
            //         {
            //             stop.RouteStops.Add(new RouteStop()
            //             {
            //                 RouteDetailId = routeDetail.Id,
            //                 StopId = stop.Id,
            //             });
            //         }
            //     }
            // }

            // await _stopRepository.UpdateRangeAsync(stopEntities, true);
            await _routeDetailRepository.UpdateRangeAsync(routeDetailEntities, true);
            await _unitOfWork.CommitTransactionAsync();
            return ApiResponse.Ok();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            return ApiResponse.Fail(e.Message);
        }
    }
    
    private async Task<List<Vertex>> PreprocessPathEntities()
    {
        var routeDetails = await _routeDetailRepository.Queryable().AsNoTracking().Select(x => new {x.Id, x.RouteId, x.RouteVarId}).ToListAsync();
        var vertices = (await _crawlEntityRepository.GenericQueryable<CrawlPath>().AsNoTracking().ToListAsync())
            .Select(x => new Vertex()
            {
                Lat = x.Lat,
                Lng = x.Lng,
                RouteDetailId = routeDetails.First(y => y.RouteId == x.RouteId && y.RouteVarId == x.RouteVarId).Id,
                Rank = x.Rank
            }).ToList();
        var duplicateItems = new List<Vertex>();
        foreach (var group in vertices.GroupBy(x => x.RouteDetailId))
        {
            var groupItems = group.ToList();
            groupItems.ForEach(item =>
            {
                if (duplicateItems.Contains(item)) return;
                var dupItems = vertices.Where(x => x.Lng.Equals(item.Lng) && x.Lat.Equals(item.Lat) && x.RouteDetailId == item.RouteDetailId).ToList();
                if (dupItems.Count >= 1)
                {
                    dupItems.RemoveAt(0);
                    duplicateItems.AddRange(dupItems);
                }
            });
        }

        if (duplicateItems.Any())
        {
            vertices.RemoveAll(vertex => duplicateItems.Contains(vertex));
        }

        foreach (var group in vertices.GroupBy(x => x.RouteDetailId))
        {
            var groupItems = group.ToList();
            foreach (var (vertex, index) in groupItems.OrderBy(x => x.Rank).WithIndex())
            {
                vertex.Rank = index + 1;
            }
        }
        
        return vertices;
    }
    

    private async Task<List<Route>> PreprocessRouteEntities()
    {
        var routes = await _crawlEntityRepository.GenericQueryable<CrawlRoute>().AsNoTracking().ToListAsync();
        var routeDetails = await _crawlEntityRepository.GenericQueryable<CrawlRouteDetail>().AsNoTracking().ToListAsync();
        var routeEntities = new List<Route>();
        foreach (var route in routes)
        {
            routeEntities.Add(new Route()
            {
                Id = route.RouteId,
                Name = route.RouteName,
                Type = route.Type,
                TimeRange = route.OperationTime,
                Unit = route.Orgs,
                RouteCode = route.RouteNo,
                BusType = Math.Round(route.Distance / 1000, 2).ToString(),
                RouteRange = route.NumOfSeats,
                RouteDetails = routeDetails
                    .Where(x => x.RouteId == route.RouteId)
                    .Select(x => new RouteDetail()
                    {
                        RouteVarId = x.RouteVarId,
                        RouteVarName = x.RouteVarName,
                        RouteNo = x.RouteNo,
                        Distance = Math.Round(route.Distance / 1000, 2),
                        EndStop = x.EndStop,
                        Outbound = x.Outbound,
                        RouteVarShortName = x.RouteVarShortName,
                        RunningTime = x.RunningTime,
                        StartStop = x.StartStop,
                        RouteId = x.RouteId,
                    }).ToList()
            });
        }

        return routeEntities;
    }

    private async Task<List<Stop>> PreprocessStopEntities()
    {
        var stopEntities = await _crawlEntityRepository.GenericQueryable<CrawlStop>()
            .AsNoTracking()
            .Select(x => new Stop()
            {
                Name = x.Name,
                AddressNo = x.AddressNo,
                Code = x.Code,
                Lat = x.Lat,
                Lng = x.Lng,
                Routes = x.Routes,
                Search = x.Search,
                Status = x.Status,
                StopType = x.StopType,
                Street = x.Street,
                RouteVarId = x.RouteVarId,
                Rank = x.Rank,
                RouteId = x.RouteId
            }).ToListAsync();
        return stopEntities;
    }

    private static async Task<List<int>> ListRouteId(HttpClient client)
    {
        var listRouteId = new List<int>();
        var response = await client.GetAsync("http://apicms.ebms.vn/businfo/getallroute");
        var result = await response.Content.ReadFromJsonAsync<List<FetchRouteDto>>();
        if (result != null)
        {
            foreach (var item in result) listRouteId.Add(item.RouteId);
        }

        return listRouteId;
    }

    private class FetchRouteDto
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string RouteNo { get; set; }
    }
}