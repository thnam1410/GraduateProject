using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.RealEstate.RouteDto;

namespace GraduateProject.Application.Extensions;

public static class CalculateUtil
{
    public static double Distance(Position x, Position y)
    {
        return Math.Sqrt(Math.Pow(x.Lat - y.Lat, 2) + Math.Pow(x.Lng - y.Lng, 2));
    }

    public static double DistanceNode(AStarNode nodeA, AStarNode nodeB)
    {
        return Distance(
            new Position() {Lat = nodeA.Lat, Lng = nodeA.Lng},
            new Position() {Lat = nodeB.Lat, Lng = nodeB.Lng}
        );
    }

    public static double GetFCost(VertexDto startNode, VertexDto targetNode, VertexDto currentNode)
    {
        var positionStartNode = new Position() {Lat = startNode.Lat, Lng = startNode.Lng};
        var positionCurrentNode = new Position() {Lat = currentNode.Lat, Lng = currentNode.Lng};
        var positionTargetNode = new Position() {Lat = targetNode.Lat, Lng = targetNode.Lng};
        
        // GCost: distance from start node
        double gCost = Distance(positionCurrentNode, positionStartNode);
        // HCost: distance from target node
        double hCost = Distance(positionCurrentNode, positionTargetNode);
        return gCost + hCost;
    }

    public static double GetHCost(VertexDto targetNode, VertexDto currentNode)
    {
        var positionCurrentNode = new Position() {Lat = currentNode.Lat, Lng = currentNode.Lng};
        var positionTargetNode = new Position() {Lat = targetNode.Lat, Lng = targetNode.Lng};
        return Distance(positionTargetNode, positionCurrentNode);
    }
    
    public static double GetGCost(VertexDto startNode, VertexDto currentNode)
    {
        var positionStartNode = new Position() {Lat = startNode.Lat, Lng = startNode.Lng};
        var positionCurrentNode = new Position() {Lat = currentNode.Lat, Lng = currentNode.Lng};
        return Distance(positionStartNode, positionCurrentNode);
    }

    public static (double, double, double) GetCost(VertexDto startNode, VertexDto targetNode, VertexDto currentNode)
    {
        var positionStartNode = new Position() {Lat = startNode.Lat, Lng = startNode.Lng};
        var positionCurrentNode = new Position() {Lat = currentNode.Lat, Lng = currentNode.Lng};
        var positionTargetNode = new Position() {Lat = targetNode.Lat, Lng = targetNode.Lng};
        
        // GCost: distance from start node
        double gCost = Distance(positionCurrentNode, positionStartNode);
        // HCost: distance from target node
        double hCost = Distance(positionCurrentNode, positionTargetNode);
        return (gCost, hCost, gCost + hCost);
    }
}