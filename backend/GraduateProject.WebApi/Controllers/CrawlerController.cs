﻿using GraduateProject.Application.Extensions;
using GraduateProject.Common.Dto;
using GraduateProject.Extensions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[Route("/api/crawler")]
public class CrawlerController : ControllerBase
{
    private const string targetUrl = "https://xe-buyt.com/tuyen-xe-buyt";
    private const string baseUrl = "https://xe-buyt.com";
    private readonly ILogger<CrawlerController> _logger;

    public CrawlerController(ILogger<CrawlerController> logger)
    {
        _logger = logger;
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

        var listRoutes = new List<RouteDto>();
        var listStops = new List<StopDto>();
        var listPath = new List<PathDto>();
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
                    var routes = await response.Content.ReadFromJsonAsync<List<RouteDto>>() ?? new List<RouteDto>();
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

                                var stopData = await responseStopData.Content.ReadFromJsonAsync<List<StopDto>>();
                                var pathData = await responsePathData.Content.ReadFromJsonAsync<PathDto>();
                                listStops.AddRange(stopData);
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

        var resultRoutes = listRoutes;
        var resultStops = listStops.GroupBy(x => x.AddressNo).Select(x => x.First());
        var resultPaths = listPath;
        return ApiResponse.Ok();
    }
}