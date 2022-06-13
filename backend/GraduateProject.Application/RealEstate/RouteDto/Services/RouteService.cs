using System.Globalization;
using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Z.EntityFramework.Plus;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class RouteService : IRouteService
{
    private readonly IRouteDetailRepository _routeDetailRepository;
    private readonly IVertexRepository _vertexRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IOptionsSnapshot<ConfigDistance> _configDistance;
    private readonly IRouteRepository _routeRepository;
    private readonly IInfoRouteSearchRepository _infoRouteSearchRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IObjectMapper _mapper;
    private readonly IPathHistoryRepository _pathHistoryRepository;

    public RouteService(IRouteDetailRepository routeDetailRepository, IVertexRepository vertexRepository, IStopRepository stopRepository, IUnitOfWork unitOfWork,IOptionsSnapshot<ConfigDistance> configDistance, IRouteRepository routeRepository, IObjectMapper mapper, IPathHistoryRepository pathHistoryRepository,IInfoRouteSearchRepository infoRouteSearchRepository)
    {
        _routeDetailRepository = routeDetailRepository;
        _vertexRepository = vertexRepository;
        _stopRepository = stopRepository;
        _configDistance = configDistance;
        _routeRepository = routeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _pathHistoryRepository = pathHistoryRepository;
        _infoRouteSearchRepository = infoRouteSearchRepository;
    }

    public async Task<RouteResponseDto> GetRoute(FindRouteRequestDto request)
    {
        var limitSearchRadius = _configDistance.Value.SearchRadius;
        // const int limitTrustNeight = 10;
        var pathHistory = await _pathHistoryRepository.Queryable().FirstOrDefaultAsync(x =>
            x.StartLat.Equals(request.StartPoint.Lat) &&
            x.StartLng.Equals(request.StartPoint.Lng) &&
            x.EndLat.Equals(request.EndPoint.Lat) &&
            x.EndLng.Equals(request.EndPoint.Lng)
        );
        if (pathHistory is not null)
        {
            if (pathHistory.IsError) throw new Exception(pathHistory.ErrorMessage);
            var resultPaths = JsonConvert.DeserializeObject<ResultPaths>(pathHistory.JsonPath) ?? new ResultPaths();
            return await RouteResponseDto(resultPaths);
        }


        var vertices = (await GetVertices()).ToList();
        var edges = (await GetEdges()).ToList();
        var graph = new Graph(vertices, edges);

        var startPointNeighborVertices = new List<VertexDto>();
        var endPointNeighborVertices = new List<VertexDto>();
        foreach (var vertex in vertices)
        {
            var distanceToStart = CalculateUtil.Distance(request.StartPoint, vertex.Position);
            var distanceToEnd = CalculateUtil.Distance(request.EndPoint, vertex.Position);
            if (distanceToStart <= limitSearchRadius)
            {
                vertex.DistanceToStart = distanceToStart;
                vertex.DistanceToEnd = CalculateUtil.Distance(request.EndPoint, vertex.Position);
                startPointNeighborVertices.Add(vertex);
            }

            if (distanceToEnd <= limitSearchRadius)
            {
                vertex.DistanceToEnd = distanceToEnd;
                endPointNeighborVertices.Add(vertex);
            }
        }

        if (!startPointNeighborVertices.Any()) throw new Exception("Route nearby not found!");
        // var trustNeighbors = new List<VertexDto>();
        var startPoint = startPointNeighborVertices.OrderBy(x => x.DistanceToStart).First();
        var endPoint = endPointNeighborVertices.OrderBy(x => x.DistanceToEnd).First();

        try
        {
            var aStarCtor = new AStar(graph, startPoint.Id, endPoint.Id);
            var resultVertices = aStarCtor.StartAlgorithms();
            // var test1 = vertices.Where(x => resultVertices.Select(y => y.Id).Contains(x.Id)).GroupBy(y => y.RouteDetailId);
            var usedVerticesIds = resultVertices.Select(x => x.Id).ToList();
            var listVertices = vertices.Where(x => usedVerticesIds.Contains(x.Id)).ToList();
            var listEdges = edges.Where(x => usedVerticesIds.Contains(x.PointAId) || usedVerticesIds.Contains(x.PointBId)).ToList();
            var resultPaths = GetResultPaths(resultVertices, listVertices, listEdges);
            await _pathHistoryRepository.AddAsync(new PathHistory()
            {
                StartLat = request.StartPoint.Lat,
                StartLng = request.StartPoint.Lng,
                EndLat = request.EndPoint.Lat,
                EndLng = request.EndPoint.Lng,
                IsError = false,
                JsonPath = JsonConvert.SerializeObject(resultPaths)
            }, true);
            return await RouteResponseDto(resultPaths);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await _pathHistoryRepository.AddAsync(new PathHistory()
            {
                StartLat = request.StartPoint.Lat,
                StartLng = request.StartPoint.Lng,
                EndLat = request.EndPoint.Lat,
                EndLng = request.EndPoint.Lng,
                IsError = true,
                ErrorMessage = e.Message
            }, true);
            throw;
        }
    }
    
    
    public async Task<List<InfoRouteSearchViewDto>> GetInfoRouteSearch(string userId)
    {
        try
        {
            var result = new List<InfoRouteSearchViewDto>();
            var infoRouteSearchList = await _infoRouteSearchRepository.Queryable()
                .Where(x=>x.UserId.ToString() == userId)
                .Include(x => x.Route)
                .OrderBy(x => x.TimeSearch).ToListAsync();
            var dateList = infoRouteSearchList.Select(x => x.TimeSearch?.ToString("dd/MM/yyyy")).Distinct()
                .ToList();
            foreach (var date in dateList)
            {
                var infoRouteSearchView = new InfoRouteSearchViewDto();
                var infoRouteSearch = infoRouteSearchList.Where(x => x.TimeSearch?.ToString("dd/MM/yyyy") == date)
                    .ToList();
                var infoRouteSearchResult = new List<InfoRouteSearchDto>();
                
                foreach (var itemInfo in infoRouteSearch)
                {
                    var infoRouteSearchDto = _mapper.Map<InfoRouteSearch, InfoRouteSearchDto>(itemInfo);
                    infoRouteSearchDto.TimeSearchDto = itemInfo.TimeSearch?.ToString("hh:mm tt");
                    infoRouteSearchResult.Add(infoRouteSearchDto);
                }
                infoRouteSearchView.Date = date;
                infoRouteSearchView.InfoRouteSearchList = infoRouteSearchResult;
                result.Add(infoRouteSearchView);
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<RouteResponseDto> RouteResponseDto(ResultPaths resultPaths)
    {
        var routeDetails = await _routeDetailRepository.Queryable()
            .Include(x => x.Route)
            .Include(x => x.Stops)
            .Where(x => resultPaths.RouteDetailListIds.Contains(x.Id))
            .ToListAsync();
        var routeStops = routeDetails.SelectMany(x => x.Stops).ToList();
        
        var pathRouteDetailIds = new HashSet<int>();
        var stops = new HashSet<StopStorageDto>();
        resultPaths.Paths.Values
            .Where(x => !x.IsSwitch)
            .SelectMany(x => x.Positions)
            .ToList()
            .ForEach(pos =>
            {
                pathRouteDetailIds.Add(pos.RouteDetailId);
                var nearestStop = routeStops.Select(x => new
                {
                    x.Id,
                    Distance = CalculateUtil.Distance(new Position() {Lat = x.Lat, Lng = x.Lng}, new Position() {Lat = pos.Lat, Lng = pos.Lng})
                }).OrderBy(x => x.Distance).FirstOrDefault();
                if (nearestStop is not null && nearestStop.Distance <= 0.05)
                {
                    var existItem = stops.FirstOrDefault(x => x.StopId == nearestStop.Id);
                    if (existItem is not null && nearestStop.Distance < existItem.Distance)
                    {
                        existItem.StopId = nearestStop.Id;
                    }
                    else
                    {
                        stops.Add(new StopStorageDto()
                        {
                            StopId = nearestStop.Id,
                            Distance = nearestStop.Distance,
                            RouteDetailId = pos.RouteDetailId
                        });
                    }
                }
            });

        return new RouteResponseDto()
        {
            Paths = resultPaths.Paths,
            // RouteDetailList = routeDetails
            //     .Where(x => pathRouteDetailIds.Contains(x.Id))
            //     .Select(routeDetail => _mapper.Map<Route, RouteDto>(routeDetail.Route))
            //     .ToList(),
            RouteDetailList = pathRouteDetailIds.Select(id =>
            {
                var routeDetail = routeDetails.First(x => x.Id == id);
                return _mapper.Map<Route, RouteDto>(routeDetail.Route);
            }).ToList(),
            Stops = stops.Select(stopDto =>
            {
                var item = _mapper.Map<Stop, StopDto>(routeStops.First(x => x.Id == stopDto.StopId));
                var routeDetail = routeDetails.FirstOrDefault(x => x.Id == stopDto.RouteDetailId);
                if (routeDetail is not null)
                {
                    item.RouteCode = routeDetail.Route.RouteCode;
                    item.RouteName = routeDetail.Route.Name;
                }
                return item;
            }).ToList()
        };
    }

    private ResultPaths GetResultPaths(List<AStarNode> vertices, List<VertexDto> listVertices, List<EdgeDto> listEdges)
    {
        int index = 1;
        var paths = new Dictionary<int, AStarPathDto>();
        var routeDetailList = new HashSet<int>();
        AStarNode previous = null;

        for (int i = 0; i < vertices.Count; i++)
        {
            var current = vertices[i];
            if (previous is null)
            {
                previous = current;
            }
            else
            {
                var isSameRoute = IsSameRoute(listVertices, previous, current);
                var item = new AStarPathDto()
                {
                    IsSwitch = !isSameRoute,
                    Positions = new List<PositionWithRouteInfo>()
                    {
                        new() {Lat = previous.Lat, Lng = previous.Lng, RouteDetailId = listVertices.First(x => x.Id == previous.Id).RouteDetailId},
                        new() {Lat = current.Lat, Lng = current.Lng, RouteDetailId = listVertices.First(x => x.Id == current.Id).RouteDetailId},
                    }
                };
                if (isSameRoute && !IsSwitchEdge(listEdges, current, previous)) routeDetailList.Add(listVertices.First(x => x.Id == current.Id).RouteDetailId);
                paths.Add(index, item);
                index++;
            }

            previous = current;
        }


        return new ResultPaths()
        {
            Paths = paths, RouteDetailListIds = routeDetailList
        };
    }

    private bool IsSameRoute(List<VertexDto> vertices, AStarNode nodeA, AStarNode nodeB)
    {
        var nodeAVertex = vertices.First(x => x.Id == nodeA.Id);
        var nodeBVertex = vertices.First(x => x.Id == nodeB.Id);
        return nodeAVertex.RouteDetailId == nodeBVertex.RouteDetailId;
    }

    private bool IsSwitchEdge(List<EdgeDto> edges, AStarNode nodeA, AStarNode nodeB)
    {
        var edge = edges.FirstOrDefault(x => (x.PointAId == nodeA.Id && x.PointBId == nodeB.Id) || (x.PointBId == nodeA.Id && x.PointAId == nodeB.Id));
        return edge?.Type == EdgeType.SwitchRoute;
    }

    private async Task<IEnumerable<EdgeDto>> GetEdges()
    {
        var edges = await _vertexRepository.GetEdgeQueryable().AsNoTracking()
            .Select(x => new EdgeDto()
            {
                PointAId = x.PointAId,
                PointBId = x.PointBId,
                EdgeDistance = x.Distance,
                Type = x.Type
            }).FromCacheAsync();
        return edges;
    }

    private async Task<IEnumerable<VertexDto>> GetVertices()
    {
        var vertices = await _vertexRepository.Queryable().AsNoTracking()
            .Select(x => new VertexDto()
            {
                Id = x.Id,
                Lat = x.Lat,
                Lng = x.Lng,
                Rank = x.Rank,
                RouteDetailId = x.RouteDetailId
            })
            .FromCacheAsync();
        return vertices;
    }

    public Task<List<Route>> GetMainRoute()
    {
        return _routeRepository.Queryable().Where(x => x.RouteDetails.Any()).ToListAsync();
    }

    public async Task CreateInfoRouteSearch(InfoRouteSearchDto request)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var infoRouteSearch = _mapper.Map<InfoRouteSearchDto, InfoRouteSearch>(request);
            infoRouteSearch.TimeSearch = DateTime.Now;
            await _infoRouteSearchRepository.AddAsync(infoRouteSearch);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollBackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task<object> GetRouteDetailsByRouteId(int routeId)
    {
        var routes = await _routeDetailRepository.Queryable()
            .Where(x => x.RouteId == routeId)
            .Include(x => x.Stops)
            .Include(x => x.Vertices)
            .Include(x => x.Route)
            .OrderBy(x => x.RouteVarId)
            .ToListAsync();
        if (routes.Any())
        {
            var forwardRoute = routes[0];
            var backwardRoute = routes[1];
            var parentRoute = forwardRoute.Route;
            return new
            {
                routeInfo = new
                {
                    parentRoute.Name, // Ten tuyen
                    parentRoute.RouteCode, // Ma so tuyen
                    parentRoute.Type, //Loại hình hoạt động
                    parentRoute.RouteRange, // Cu ly
                    parentRoute.BusType, // Loai xe
                    parentRoute.TimeRange, //Tgian hoat dong
                    parentRoute.Unit, // Don vi dam nhan
                },
                forwardRouteStops = forwardRoute.Stops.Select(x => new
                {
                    x.Name,
                    position = new Position() {Lat = x.Lat, Lng = x.Lng},
                }), // cac tram dung -> hien marker
                forwardRoutePos = forwardRoute.Vertices.OrderBy(x => x.Rank).Select(x => new Position() {Lat = x.Lat, Lng = x.Lng}),

                backwardRouteStops = backwardRoute.Stops.Select(x => new
                {
                    x.Name,
                    position = new Position() {Lat = x.Lat, Lng = x.Lng},
                }),
                backwardRoutePos = backwardRoute.Vertices.OrderBy(x => x.Rank).Select(x => new Position() {Lat = x.Lat, Lng = x.Lng}),
            };
        }

        throw new Exception();
    }

    public async Task<object> GetBusStopNearby(Position position)
    {
        var stops = await _stopRepository.Queryable().Select(x => new
        {
            x.Lat, x.Lng, x.Name, x.Routes, Address = x.AddressNo, x.Code
        }).FromCacheAsync();
        return stops.Where(x => CalculateUtil.Distance(position, new Position() {Lat = x.Lat, Lng = x.Lng}) <= 1).ToList();
    }

    private class ResultPaths
    {
        public Dictionary<int, AStarPathDto> Paths { get; set; }
        public HashSet<int> RouteDetailListIds { get; set; }
    }

    private class StopStorageDto
    {
        public double Distance { get; set; }
        public int StopId { get; set; }
        public int RouteDetailId { get; set; }
    }
}