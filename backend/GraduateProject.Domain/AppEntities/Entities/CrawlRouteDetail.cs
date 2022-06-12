using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlRouteDetail: Entity<Guid>
{
    public double Distance { get; set; } // "13501",
    public string EndStop { get; set; } // "Bến xe Miền Tây",
    public bool Outbound { get; set; } // "true",
    public int RouteId { get; set; } // "2",
    public string RouteNo { get; set; } // "2",
    public int RouteVarId { get; set; } // "3",
    public string RouteVarName { get; set; } // Bến Thành - BX Miền Tây",
    public string RouteVarShortName { get; set; } // "BX Miền Tây",
    public int RunningTime { get; set; } // "47",
    public string StartStop { get; set; } // "Công Trường Mê Linh"
}