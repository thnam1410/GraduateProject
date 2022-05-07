using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.RealEstate.RouteDto;

namespace GraduateProject.Application.Extensions;

public static class CalculateUtil
{
    // public const int RouteAlphaConstant = 100;
    
    static double ToRadians(
        double angleIn10ThofaDegree)
    {
        // Angle in 10th
        // of a degree
        return (angleIn10ThofaDegree * Math.PI) / 180;
    }

    public static double Distance(Position x, Position y)
    {
        double lng1 = ToRadians(x.Lng);
        double lng2 = ToRadians(y.Lng);
        double lat1 = ToRadians(x.Lat);
        double lat2 = ToRadians(y.Lat);

        // Haversine formula
        double dlon = lng2 - lng1;
        double dlat = lat2 - lat1;
        double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Pow(Math.Sin(dlon / 2), 2);

        double c = 2 * Math.Asin(Math.Sqrt(a));

        // Radius of earth in
        // kilometers. Use 3956
        // for miles
        double r = 6371;

        // calculate the result
        return (c * r);
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