using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlStop: Entity<Guid>
{
    public string AddressNo { get; set; } //: "66"
    public string Code { get; set; } //: "CN01"
    public string Lat { get; set; } //: "10.804758"
    public string Lng { get; set; } //: "106.667644"
    public string Name { get; set; } //: "Bãi Xe Phổ Quang"
    public string Routes { get; set; } //: "C010"
    public string Search { get; set; } //: "BXPQ 66 PQ"
    public string Status { get; set; } //: "Đang khai thác"
    public string StopID { get; set; } //: "1006842"
    public string StopType { get; set; } //: "Bến xe"
    public string Street { get; set; } //: "Phổ Quang"
}