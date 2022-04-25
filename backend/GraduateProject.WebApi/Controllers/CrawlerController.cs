﻿using GraduateProject.Common.Dto;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path = GraduateProject.Domain.AppEntities.Entities.Path;
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
    private readonly IPathRepository _pathRepository;

    public CrawlerController(ILogger<CrawlerController> logger, ICrawlEntityRepository crawlEntityRepository, IUnitOfWork unitOfWork, IPathRepository pathRepository, IRouteDetailRepository routeDetailRepository, IStopRepository stopRepository, IRouteRepository routeRepository)
    {
        _logger = logger;
        _crawlEntityRepository = crawlEntityRepository;
        _unitOfWork = unitOfWork;
        _pathRepository = pathRepository;
        _routeDetailRepository = routeDetailRepository;
        _stopRepository = stopRepository;
        _routeRepository = routeRepository;
    }

    [HttpGet("start")]
    public async Task<ApiResponse> HandleCrawlerRoutes()
    {
        HttpClient client = new HttpClient();
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

                                var pathUrl = $"https://api.xe-buyt.com/businfo/getpathsbyvar/{routeId}_1/{routeVarId}";
                                _logger.LogInformation($"Fetching API: {pathUrl}");
                                var responsePathData = await client.GetAsync(pathUrl);
                                responsePathData.EnsureSuccessStatusCode();

                                // var stopData = await responseStopData.Content.ReadFromJsonAsync<List<CrawlStop>>();
                                var pathData = await responsePathData.Content.ReadFromJsonAsync<CrawlPathDto>();
                                pathData.RouteId = routeId;
                                pathData.RouteVarId = routeVarId;
                                // listStops.AddRange(stopData);
                                listPath.Add(pathData);
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


        var listCrawlEntityRoutes = listRoutes;
        var listCrawlEntityStops = listStops.GroupBy(x => x.AddressNo).Select(x => x.First()).ToList();
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
                });
            }
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _crawlEntityRepository.AddRangeCrawlRouteAsync(listCrawlEntityRoutes);
            await _crawlEntityRepository.AddRangeCrawlPathAsync(listCrawlEntityPath);
            await _crawlEntityRepository.AddRangeCrawlStopAsync(listCrawlEntityStops);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
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
            var routeDetailEntities = await _routeDetailRepository.Queryable().AsNoTracking().ToListAsync();
            List<Stop> stopEntities = await _stopRepository.Queryable().ToListAsync();
            foreach (var stop in stopEntities)
            {
                var listRouteCode = stop.Routes.Split(",").Select(x => x.Trim());
                foreach (var routeCode in listRouteCode)
                {
                    var routeDetail = routeDetailEntities.FirstOrDefault(x => string.Equals(x.RouteNo, routeCode, StringComparison.CurrentCultureIgnoreCase));
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
            List<Path> pathEntities = await PreprocessPathEntities();
            await _pathRepository.AddRangeAsync(pathEntities, true);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
        }
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

    private async Task<List<Path>> PreprocessPathEntities()
    {
        var routeDetails = await _routeDetailRepository.Queryable().AsNoTracking().Select(x => new {x.Id, x.RouteId, x.RouteVarId}).ToListAsync();
        return (await _crawlEntityRepository.GenericQueryable<CrawlPath>().AsNoTracking().ToListAsync())
            .Select(x => new Path()
            {
                Lat = x.Lat,
                Lng = x.Lng,
                RouteDetailId = routeDetails.First(y => y.RouteId == x.RouteId && y.RouteVarId == x.RouteVarId).Id,
            }).ToList();
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
            }).ToListAsync();
        return stopEntities;
    }
    
    
}