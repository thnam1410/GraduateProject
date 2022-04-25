using Newtonsoft.Json;

namespace GraduateProject.Common.Dto;

public class CrawlPathDto
{
    public int? RouteId { get; set; }
    public int? RouteVarId { get; set; }
    [JsonProperty(PropertyName = "lat")]
    public List<double> Lat { get; set; } // vĩ độ
    [JsonProperty(PropertyName = "lng")]
    public List<double> Lng { get; set; } // kinh độ
}