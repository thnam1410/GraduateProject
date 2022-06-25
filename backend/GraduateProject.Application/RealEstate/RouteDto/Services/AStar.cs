using GraduateProject.Application.Extensions;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Extensions;
using Microsoft.Extensions.Logging;

namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public class AStar
{
    private static CustomHeap<AStarNode> openList { get; set; }
    private static List<Guid> closeList { get; set; }
    private static AStarNode startNode;
    private static AStarNode targetNode;
    private readonly IGraph _graph;
    private static AStarNode tempCurrentNode;

    public AStar(IGraph graph, Guid startNodeId, Guid targetNodeId)
    {
        _graph = graph;
        openList = new CustomHeap<AStarNode>(1_000_000);
        closeList = new List<Guid>();
        var firstNodeVertex = _graph.Vertices.FirstOrDefault(x => x.Id == startNodeId) ?? throw new Exception("Can not find path for start node");
        var targetNodeVertex = _graph.Vertices.FirstOrDefault(x => x.Id == targetNodeId) ?? throw new Exception("Can not find path for target node");

        startNode = new AStarNode(firstNodeVertex.Id, firstNodeVertex.Lat, firstNodeVertex.Lng, targetNodeVertex);
        targetNode = new AStarNode(targetNodeVertex.Id, targetNodeVertex.Lat, targetNodeVertex.Lng);
        tempCurrentNode = null;
    }

    public List<AStarNode> StartAlgorithms()
    {
        openList.Add(startNode);
        while (openList.Any())
        {
            if (closeList.Count >= 10000) throw new Exception("Algorithms out of run time, Can not find path!");
            var count = openList.Count;
            //1: Find smallest f_cost node - Heap data structure
            //Formula compare: (A.FCost < B.FCost) || (A.FCost == B.FCost && A.HCost < B.HCost)
            AStarNode currentNode = openList.RemoveFirst();
            Console.WriteLine($"Before: {count}, After: {openList.Count}, CloseList: {closeList.Count}");

            //2: Remove node
            tempCurrentNode = currentNode;
            closeList.Add(currentNode.Id);

            if (currentNode.Id == targetNode.Id) // 1m
            {
                targetNode.ParentNode = tempCurrentNode;
                return RetracePath(startNode, targetNode);
            }

            //3: Loop through neighbors
            GenerateNeighborsToOpenList(tempCurrentNode);
        }

        if (openList.Any()) throw new Exception("Can not find path!");
        return new List<AStarNode>();
    }

    private void GenerateNeighborsToOpenList(AStarNode currentNode)
    {
        var neighbors = _graph.GetAStarNeighbors(startNode, targetNode, currentNode);
        
        //High priority for main routes
        var isAddedMainRoute = AddNeighborsToOpenList(currentNode, neighbors);
    }

    private static bool AddNeighborsToOpenList(AStarNode currentNode, ICollection<AStarNode> neighbors)
    {
        bool flagAdded = false;
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
                    flagAdded = true;
                    openList.Add(neighborNode);
                }
            }
        }

        return flagAdded;
    }

    public List<AStarNode> RetracePath(AStarNode startNode, AStarNode endNode)
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
}