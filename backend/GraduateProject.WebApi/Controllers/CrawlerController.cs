using System.Text.RegularExpressions;
using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Common.Dto;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route = GraduateProject.Domain.AppEntities.Entities.Route;

namespace GraduateProject.Controllers;

[Route("/api/crawler")]
public class CrawlerController : ControllerBase
{
    private const string targetUrl = "https://xe-buyt.com/tuyen-xe-buyt";
    private const string baseUrl = "https://xe-buyt.com";
    private readonly ILogger<CrawlerController> _logger;
    private readonly ICrawlEntityRepository _crawlEntityRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStopRepository _stopRepository;
    private readonly IRouteDetailRepository _routeDetailRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IVertexRepository _vertexRepository;

    public CrawlerController(ILogger<CrawlerController> logger, ICrawlEntityRepository crawlEntityRepository, IUnitOfWork unitOfWork,
        IVertexRepository vertexRepository, IRouteDetailRepository routeDetailRepository, IStopRepository stopRepository, IRouteRepository routeRepository)
    {
        _logger = logger;
        _crawlEntityRepository = crawlEntityRepository;
        _unitOfWork = unitOfWork;
        _vertexRepository = vertexRepository;
        _routeDetailRepository = routeDetailRepository;
        _stopRepository = stopRepository;
        _routeRepository = routeRepository;
    }

    [HttpGet("start")]
    public async Task<ApiResponse> HandleCrawlerRoutes()
    {
        HttpClient client = new HttpClient();
        var listUrls = await ListUrls(client);

        var listRoutes = new List<CrawlRoute>();
        var listStops = new List<CrawlStop>();
        var listPath = new List<CrawlPathDto>();
        foreach (var url in listUrls)
        {
            var resUrl = await client.GetAsync(url);
            var result = await resUrl.Content.ReadAsStringAsync();
            var document = new HtmlDocument();
            document.LoadHtml(result);

            #region Crawl dữ liệu các trạm dừng trên lộ trình các tuyến

            var accordion = document.GetElementbyId("accordion");
            var divRouteIdAttribute = accordion.Descendants("div").SelectMany(e => e.Attributes).FirstOrDefault(x => x.Name == "id");
            if (divRouteIdAttribute is not null)
            {
                var stringRouteId = divRouteIdAttribute.Value.Split("v")[0].Replace("r", "");
                var canParseInt = int.TryParse(stringRouteId, out var routeId);
                if (!canParseInt) throw new Exception($"Can not parse id get from: {divRouteIdAttribute.Value}");

                var routeUrl = $"https://api.xe-buyt.com/businfo/getvarsbyroute/{routeId}_1";
                try
                {
                    _logger.LogInformation($"Fetching API: {routeUrl}");
                    var response = await client.GetAsync(routeUrl);
                    response.EnsureSuccessStatusCode();
                    var routes = await response.Content.ReadFromJsonAsync<List<CrawlRoute>>() ?? new List<CrawlRoute>();
                    if (routes.Any())
                    {
                        listRoutes.AddRange(routes);
                        var routeVarIds = routes.Select(x => int.Parse(x.RouteVarId));
                        foreach (var routeVarId in routeVarIds)
                        {
                            try
                            {
                                var stopUrl = $"https://api.xe-buyt.com/businfo/getstopsbyvar/{routeId}_1/{routeVarId}";
                                _logger.LogInformation($"Fetching API: {stopUrl}");
                                var responseStopData = await client.GetAsync(stopUrl);
                                responseStopData.EnsureSuccessStatusCode();

                                // var pathUrl = $"https://api.xe-buyt.com/businfo/getpathsbyvar/{routeId}_1/{routeVarId}";
                                // _logger.LogInformation($"Fetching API: {pathUrl}");
                                // var responsePathData = await client.GetAsync(pathUrl);
                                // responsePathData.EnsureSuccessStatusCode();

                                var stopData = await responseStopData.Content.ReadFromJsonAsync<List<CrawlStop>>();
                                // var pathData = await responsePathData.Content.ReadFromJsonAsync<CrawlPathDto>();
                                
                                stopData.ForEach(stop => stop.RouteVarId = routeVarId);
                                // pathData.RouteId = routeId;
                                
                                // pathData.RouteVarId = routeVarId;
                                listStops.AddRange(stopData);
                                // listPath.Add(pathData);
                            }
                            catch (Exception e)
                            {
                                _logger.LogInformation($"Fetch api fail at routeVarId: {routeVarId} - routeUrl: {routeUrl}");
                                continue;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"Fetch api fail at url: {routeUrl}");
                    continue;
                }

                await Task.Delay(1000);
            }

            #endregion
        }


        // var listCrawlEntityRoutes = listRoutes;
        var listCrawlEntityStops = listStops.GroupBy(x => x.AddressNo).Select(x => x.First()).ToList();
        // var listCrawlEntityPath = new List<CrawlPath>();
        // foreach (var path in listPath)
        // {
        //     for (int i = 0; i < path.Lat.Count(); i++)
        //     {
        //         listCrawlEntityPath.Add(new CrawlPath()
        //         {
        //             RouteId = path.RouteId.Value,
        //             RouteVarId = path.RouteVarId.Value,
        //             Lat = path.Lat[i],
        //             Lng = path.Lng[i],
        //             Rank = i + 1
        //         });
        //     }
        // }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            // await _crawlEntityRepository.AddRangeCrawlRouteAsync(listCrawlEntityRoutes);
            _logger.LogInformation("Crawl Routes into DB successfully!");
            // await _crawlEntityRepository.AddRangeCrawlPathAsync(listCrawlEntityPath);
            _logger.LogInformation("Crawl Path into DB successfully!");
            await _crawlEntityRepository.AddRangeCrawlStopAsync(listCrawlEntityStops);
            _logger.LogInformation("Crawl Stops into DB successfully!");
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
        }

        return ApiResponse.Ok();
    }

    private static async Task<List<string>> ListUrls(HttpClient client)
    {
        var listUrls = new List<string>();
        using (var response = await client.GetAsync(targetUrl))
        {
            using (var content = response.Content)
            {
                var result = await content.ReadAsStringAsync();
                var document = new HtmlDocument();
                document.LoadHtml(result);
                var body = document.DocumentNode.SelectSingleNode("//*[@id='divResult']");
                var nodes = body.ChildNodes.Where(x => x.Attributes.Any(a => a.Value == "cms-button"));
                var urls = nodes.SelectMany(x => x.Attributes).Where(x => x.Name == "href").Select(x => $"{baseUrl}{x.Value}");
                listUrls.AddRange(urls);
            }
        }

        return listUrls;
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
            var routeDetailEntities = await _routeDetailRepository.Queryable().AsNoTracking()
                .Include(x => x.Route).ToListAsync();
            List<Stop> stopEntities = await _stopRepository.Queryable().ToListAsync();
            foreach (var stop in stopEntities)
            {
                var listRouteCode = stop.Routes.Split(",").Select(x => x.Trim());
                foreach (var routeCode in listRouteCode)
                {
                    var routeDetail = routeDetailEntities.FirstOrDefault(x => string.Equals(x.Route.RouteCode, routeCode, StringComparison.CurrentCultureIgnoreCase) && x.RouteVarId == stop.RouteVarId);
                    if (routeDetail is not null && routeDetail.RouteStops.All(x => x.StopId != stop.Id && x.RouteDetailId != routeDetail.Id))
                    {
                        stop.RouteStops.Add(new RouteStop()
                        {
                            RouteDetailId = routeDetail.Id,
                            StopId = stop.Id,
                        });
                    }
                }
            }

            await _stopRepository.UpdateRangeAsync(stopEntities, true);
            await _unitOfWork.CommitTransactionAsync();
            return ApiResponse.Ok();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            return ApiResponse.Fail(e.Message);
        }
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
        double litmitDistance = 0.5; //500m
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

    [HttpGet("start-crawl-infos")]
    public async Task<ApiResponse> HandleCrawlerRoutesInfo()
    {
        HttpClient httpClient = new HttpClient();
        var listUrls = await ListUrls(httpClient);
        var routes = await _routeRepository.ToListAsync();
        foreach (var url in listUrls)
        {
            var resUrl = await httpClient.GetAsync(url);
            var result = await resUrl.Content.ReadAsStringAsync();
            var document = new HtmlDocument();
            document.LoadHtml(result);

            var accordion = document.GetElementbyId("accordion");
            var divRouteIdAttribute = accordion.Descendants("div").SelectMany(e => e.Attributes).FirstOrDefault(x => x.Name == "id");
            var stringRouteId = divRouteIdAttribute?.Value.Split("v")[0].Replace("r", "");
            if (stringRouteId is null) continue;
            var route = routes.FirstOrDefault(x => x.Id == int.Parse(stringRouteId));
            _logger.LogInformation("Fetch route:" + stringRouteId);
            if (route is not null)
            {
                var unitText = document.DocumentNode.Descendants().Where(x => x.HasClass("cms-col-left-align") && x.Name == "td")
                    .FirstOrDefault(x => x.InnerText.Contains("Đơn vị đảm nhận:"))?.ParentNode?.ChildNodes
                    ?.LastOrDefault()?.InnerText;
                if (!string.IsNullOrWhiteSpace(unitText))
                {
                    var cleanUnitText = Regex.Replace(unitText, "<.*?>", string.Empty);
                    route.Unit = cleanUnitText;
                }

                var routeCode = document.DocumentNode.Descendants().Where(x => x.HasClass("cms-col-left-align") && x.Name == "td")
                    .FirstOrDefault(x => x.InnerText.Contains("Mã số tuyến:"))?.ParentNode?.ChildNodes
                    ?.LastOrDefault()?.InnerText;
                if (!string.IsNullOrWhiteSpace(unitText))
                {
                    route.RouteCode = routeCode;
                }

                var liNodes = document.DocumentNode
                    .Descendants()
                    .First(n => n.HasClass("list") && n.Name == "ul")
                    .ChildNodes.Where(n => n.Name == "li").ToList();
                if (liNodes.Any())
                {
                    route.Type = liNodes[0].InnerText.Split(": ")[1];
                    route.BusType = liNodes[1].InnerText.Split(": ")[1];
                    route.RouteRange = liNodes[2].InnerText.Split(": ")[1];
                    route.TimeRange = liNodes[3].InnerText.Split(": ")[1];
                }
            }
        }

        await _routeRepository.UpdateRangeAsync(routes, true);
        return ApiResponse.Ok();
    }


    private async Task<List<Route>> PreprocessRouteEntities()
    {
        var routesDetails = await _crawlEntityRepository.GenericQueryable<CrawlRoute>().AsNoTracking().ToListAsync();
        var routes = new List<Route>();
        foreach (var routeGroup in routesDetails.GroupBy(x => x.RouteId))
        {
            routes.Add(new Route()
            {
                Id = int.Parse(routeGroup.Key),
                Name = string.Join("-", routeGroup.ToList().Select(x => x.RouteVarShortName)),
                RouteDetails = routeGroup.ToList().Select(x => new RouteDetail()
                {
                    RouteVarId = int.Parse(x.RouteVarId),
                    RouteVarName = x.RouteVarName,
                    RouteNo = x.RouteNo,
                    Distance = string.IsNullOrWhiteSpace(x.Distance) ? 0 : double.Parse(x.Distance),
                    EndStop = x.EndStop,
                    Outbound = bool.Parse(x.Outbound),
                    RouteVarShortName = x.RouteVarShortName,
                    RunningTime = string.IsNullOrWhiteSpace(x.RunningTime) ? 0 : int.Parse(x.RunningTime),
                    StartStop = x.StartStop,
                }).ToList()
            });
        }

        return routes;
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

    private async Task<List<Stop>> PreprocessStopEntities()
    {
        var stopEntities = await _crawlEntityRepository.GenericQueryable<CrawlStop>()
            .AsNoTracking()
            .Select(x => new Stop()
            {
                Name = x.Name,
                AddressNo = x.AddressNo,
                Code = x.Code,
                Lat = double.Parse(x.Lat),
                Lng = double.Parse(x.Lng),
                Routes = x.Routes,
                Search = x.Search,
                Status = x.Status,
                StopType = x.StopType,
                Street = x.Street,
                RouteVarId = x.RouteVarId
            }).ToListAsync();
        return stopEntities;
    }

    private class EdgeSimpleDto
    {
        public Guid Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int RouteId { get; set; }
    }
}