using GraduateProject.Application.Extensions;
using GraduateProject.Domain.Common;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class AStarNodeBusStop: IHeapItem<AStarNodeBusStop>
{
    public int Id { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }

    public double GCost { get; set; }
    public double HCost { get; set; }

    public double FCost
    {
        get
        {
            return GCost + HCost;
        }
    }
    
    public AStarNodeBusStop? ParentNode { get; set; }
    public Position Position { get; set; }

        
    public AStarNodeBusStop(int id, double lat, double lng, StopDto targetStop)
    {
        Id = id;
        Lat = lat;
        Lng = lng;
        GCost = 0;
        HCost = CalculateUtil.Distance(
            new Position() {Lat = lat, Lng = lng},
            new Position() {Lat = targetStop.Lat, Lng = targetStop.Lng}
        );
        Position = new Position() {Lng = lng, Lat = lat};
    }
    
    public AStarNodeBusStop(int id, double lat, double lng)
    {
        Id = id;
        Lat = lat;
        Lng = lng;
        Position = new Position() {Lng = lng, Lat = lat};
    }
    
    public AStarNodeBusStop(int id, double lat, double lng, AStarNodeBusStop startNode, AStarNodeBusStop targetNode)
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

    public int CompareTo(AStarNodeBusStop? other)
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