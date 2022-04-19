using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlRoute: Entity<Guid>
{
    public string Distance { get; set; } // "13501",
    public string EndStop { get; set; } // "Bến xe Miền Tây",
    public string Outbound { get; set; } // "true",
    public string RouteId { get; set; } // "2",
    public string RouteNo { get; set; } // "2",
    public string RouteVarId { get; set; } // "3",
    public string RouteVarName { get; set; } // Bến Thành - BX Miền Tây",
    public string RouteVarShortName { get; set; } // "BX Miền Tây",
    public string RunningTime { get; set; } // "47",
    public string StartStop { get; set; } // "Công Trường Mê Linh"
}