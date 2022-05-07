using GraduateProject.Application.Common.Dto;
using GraduateProject.Application.Extensions;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class AStarNode : IHeapItem<AStarNode>
{
    public Guid Id { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }

    public double GCost { get; set; }
    public double HCost { get; set; }

    public double FCost
    {
        get { return GCost + HCost; }
    }

    public AStarNode? ParentNode { get; set; }
    public bool IsSwitchRouteNode { get; set; } = false;
    public Position Position { get; set; }

    public AStarNode()
    {
    }

    public AStarNode(Guid id, double lat, double lng)
    {
        Id = id;
        Lat = lat;
        Lng = lng;
        Position = new Position() {Lng = lng, Lat = lat};
    }

    public AStarNode(Guid id, double lat, double lng, VertexDto vertex)
    {
        Id = id;
        Lat = lat;
        Lng = lng;
        GCost = 0;
        HCost = CalculateUtil.Distance(
            new Position() {Lat = lat, Lng = lng},
            new Position() {Lat = vertex.Lat, Lng = vertex.Lng}
        );
        Position = new Position() {Lng = lng, Lat = lat};
    }

    public AStarNode(Guid id, double lat, double lng, AStarNode startNode, AStarNode targetNode)
    {
        Id = id;
        Lat = lat;
        Lng = lng;
        GCost = CalculateUtil.Distance(
            new Position() {Lat = lat, Lng = lng},
            new Position() {Lat = startNode.Lat, Lng = startNode.Lng}
        );
        HCost = CalculateUtil.Distance(
            new Position() {Lat = lat, Lng = lng},
            new Position() {Lat = targetNode.Lat, Lng = targetNode.Lng}
        );
        Position = new Position() {Lng = lng, Lat = lat};
    }

    public int HeapIndex { get; set; }

    public int CompareTo(AStarNode? other)
    {
        if (other is null) return -1;
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0)
        {
            compare = HCost.CompareTo(other.HCost);
        }
        return -compare;
    }
}