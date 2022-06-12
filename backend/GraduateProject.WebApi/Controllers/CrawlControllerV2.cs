using GraduateProject.Application.Extensions;
using GraduateProject.Common.Dto;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

public class CrawlControllerV2 : ControllerBase
{
    private readonly ILogger<CrawlerController> _logger;
    private readonly IObjectMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICrawlEntityRepository _crawlEntityRepository;

    public CrawlControllerV2(ILogger<CrawlerController> logger, IObjectMapper mapper, IUnitOfWork unitOfWork, ICrawlEntityRepository crawlEntityRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _crawlEntityRepository = crawlEntityRepository;
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
            if(resultRouteInfo is not null) listRoutes.Add(resultRouteInfo);
            
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
                            stopData.ForEach(stop => stop.RouteVarId = routeVarId);
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