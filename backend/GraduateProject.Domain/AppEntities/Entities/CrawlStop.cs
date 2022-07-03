using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlStop: Entity<Guid>
{
    public string? AddressNo { get; set; } //: "66"
    public string? Code { get; set; } //: "CN01"
    public double Lat { get; set; } //: "10.804758"
    public double Lng { get; set; } //: "106.667644"
    public string? Name { get; set; } //: "Bãi Xe Phổ Quang"
    public string? Routes { get; set; } //: "C010"
    public string? Search { get; set; } //: "BXPQ 66 PQ"
    public string? Status { get; set; } //: "Đang khai thác"
    public int StopId { get; set; } //: "1006842"
    public string? StopType { get; set; } //: "Bến xe"
    public string? Street { get; set; } //: "Phổ Quang"
    public string? Ward { get; set; }
    public string? Zone { get; set; }
    public int RouteVarId { get; set; }
    public int RouteId { get; set; }
    public int Rank { get; set; }
    
}