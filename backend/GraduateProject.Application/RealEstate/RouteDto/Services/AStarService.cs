using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class AStarService : IAStarService
{
    private readonly IVertexRepository _vertexRepository;

    public AStarService(IVertexRepository vertexRepository)
    {
        _vertexRepository = vertexRepository;
    }


    #region BusStopPath

    public async Task<List<AStarNodeBusStop>> StartAlgorithmsBusStopPaths(StopDto startPoint, StopDto endPoint, List<StopDto> stopDtos)
    {
        CustomHeap<AStarNodeBusStop> _openList = new CustomHeap<AStarNodeBusStop>(1_000_000);
        List<int> _closeList = new List<int>();
        AStarNodeBusStop _startNode = new AStarNodeBusStop(startPoint.Id, startPoint.Lat, startPoint.Lng, endPoint);
        ;
        AStarNodeBusStop _targetNode = new AStarNodeBusStop(endPoint.Id, endPoint.Lat, endPoint.Lng);
        AStarNodeBusStop _tempCurrentNode;

        var edges = await _vertexRepository.GetBusStopEdgeQueryable().ToListAsync();

        _openList.Add(_startNode);

        while (_openList.Any())
        {
            if (_closeList.Count >= 10000) throw new Exception("Algorithms out of run time, Can not find path!");
            AStarNodeBusStop currentNode = _openList.RemoveFirst();

            //2: Remove node
            _tempCurrentNode = currentNode;
            _closeList.Add(currentNode.Id);

            if (currentNode.Id == _targetNode.Id) // 1m
            {
                _targetNode.ParentNode = _tempCurrentNode;
                return RetracePathBusStop(_startNode, _targetNode);
            }

            //3: Loop through neighbors
            var neighbors = GetAStarNeighborsBusStop(_startNode, _targetNode, currentNode, edges, stopDtos);

            //Add neighbors to openList
            foreach (var neighborNode in neighbors)
            {
                if (_closeList.Contains(neighborNode.Id)) continue;

                double newMovementCostToNeighbor = currentNode.GCost + CalculateUtil.DistanceNodeBusStop(currentNode, neighborNode);
                var openListContainNode = _openList.ContainsId(neighborNode.HeapIndex, neighborNode.Id);
                if (newMovementCostToNeighbor < neighborNode.GCost || !openListContainNode)
                {
                    neighborNode.GCost = newMovementCostToNeighbor;
                    neighborNode.HCost = CalculateUtil.DistanceNodeBusStop(neighborNode, _targetNode);
                    neighborNode.ParentNode = currentNode;
                    if (!openListContainNode)
                    {
                        _openList.Add(neighborNode);
                    }
                }
            }
        }

        if (_openList.Any()) throw new Exception("Can not find path!");
        return new List<AStarNodeBusStop>();
    }

    private List<AStarNodeBusStop> RetracePathBusStop(AStarNodeBusStop startNode, AStarNodeBusStop endNode)
    {
        List<AStarNodeBusStop> paths = new List<AStarNodeBusStop>();
        AStarNodeBusStop currentNode = endNode;
        while (currentNode.Id != startNode.Id)
        {
            paths.Add(currentNode);
            if (currentNode.ParentNode != null) currentNode = currentNode.ParentNode;
            else break;
        }

        paths.Reverse();
        return paths;
    }

    private ICollection<AStarNodeBusStop> GetAStarNeighborsBusStop(AStarNodeBusStop startNode, AStarNodeBusStop targetNode, AStarNodeBusStop currentNode,
        List<BusStopEdge> busStopEdges, List<StopDto> stopDtos)
    {
        var edges = busStopEdges
            .Where(x => x.PointAId == currentNode.Id)
            .ToList();
        var pointBIds = edges
            .Select(x => x.PointBId)
            .ToList();
        return stopDtos.Where(x => pointBIds.Contains(x.Id))
            .Select(x => new AStarNodeBusStop(x.Id, x.Lat, x.Lng, startNode, targetNode))
            .ToList();
    }

    #endregion


    #region RoutePath
    public async Task<List<AStarNode>> StartAlgorithmsRoutePaths(List<VertexDto> vertices, List<EdgeDto> edges, Guid startNodeId, Guid targetNodeId)
    {
        await Task.CompletedTask;
        CustomHeap<AStarNode> openList = new CustomHeap<AStarNode>(1_000_000);
        List<Guid> closeList = new List<Guid>();
        var firstNodeVertex = vertices.FirstOrDefault(x => x.Id == startNodeId) ?? throw new Exception("Can not find path for start node");
        var targetNodeVertex = vertices.FirstOrDefault(x => x.Id == targetNodeId) ?? throw new Exception("Can not find path for target node");
        AStarNode startNode = new AStarNode(firstNodeVertex.Id, firstNodeVertex.Lat, firstNodeVertex.Lng, targetNodeVertex);
        AStarNode targetNode = new AStarNode(targetNodeVertex.Id, targetNodeVertex.Lat, targetNodeVertex.Lng);
        AStarNode tempCurrentNode = null;

        openList.Add(startNode);
        while (openList.Any())
        {
            if (closeList.Count >= 10000) throw new Exception("Algorithms out of run time, Can not find path!");
            var count = openList.Count;
            //1: Find smallest f_cost node - Heap data structure
            //Formula compare: (A.FCost < B.FCost) || (A.FCost == B.FCost && A.HCost < B.HCost)
            AStarNode currentNode = openList.RemoveFirst();
            Console.WriteLine("Before: {0}, After: {1}, CloseList: {2}", count, openList.Count, closeList.Count);

            //2: Remove node
            tempCurrentNode = currentNode;
            closeList.Add(currentNode.Id);

            if (currentNode.Id == targetNode.Id) // 1m
            {
                targetNode.ParentNode = tempCurrentNode;
                return RetracePathRoute(startNode, targetNode);
            }

            //3: Loop through neighbors
            var neighbors = GetAStarNeighbors(startNode, targetNode, currentNode, edges, vertices);

            //High priority for main routes
            foreach (var neighborNode in neighbors)
            {
                if (closeList.Contains(neighborNode.Id)) continue;

                double newMovementCostToNeighbor = currentNode.GCost + CalculateUtil.DistanceNode(currentNode, neighborNode);
                var openListContainNode = openList.ContainsId(neighborNode.HeapIndex, neighborNode.Id);
                if (newMovementCostToNeighbor < neighborNode.GCost || !openListContainNode)
                {
                    neighborNode.GCost = newMovementCostToNeighbor;
                    neighborNode.HCost = CalculateUtil.DistanceNode(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;
                    if (!openListContainNode)
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        if (openList.Any()) throw new Exception("Can not find path!");
        return new List<AStarNode>();
    }

    private List<AStarNode> RetracePathRoute(AStarNode startNode, AStarNode endNode)
    {
        List<AStarNode> paths = new List<AStarNode>();
        AStarNode currentNode = endNode;
        while (currentNode.Id != startNode.Id)
        {
            paths.Add(currentNode);
            if (currentNode.ParentNode != null) currentNode = currentNode.ParentNode;
            else break;
        }

        paths.Reverse();
        return paths;
    }

    private ICollection<AStarNode> GetAStarNeighbors(AStarNode startNode, AStarNode targetNode, AStarNode currentNode,
        List<EdgeDto> edges, List<VertexDto> vertices, EdgeType? edgeType = null)
    {
        var currentNodeEdge = edges
            .Where(x => x.PointAId == currentNode.Id)
            .WhereIf(edgeType.HasValue, x => x.Type == edgeType)
            .ToList();
        var pointBIds = currentNodeEdge
            .Select(x => x.PointBId)
            .ToList();
        return vertices.Where(x => pointBIds.Contains(x.Id))
            .Select(x =>
            {
                var edge = currentNodeEdge.First(y => y.PointBId == x.Id);
                var value = new AStarNode(x.Id, x.Lat, x.Lng, startNode, targetNode);
                if (edge.Type == EdgeType.SwitchRoute) value.IsSwitchRouteNode = true;
                return value;
            })
            .ToList();
    }
    #endregion
}