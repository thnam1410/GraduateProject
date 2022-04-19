using Newtonsoft.Json;

namespace GraduateProject.Common.Dto;

public class CrawlPathDto
{
    public int? RouteId { get; set; }
    [JsonProperty(PropertyName = "lat")]
    public List<decimal> Lat { get; set; } // vĩ độ
    [JsonProperty(PropertyName = "lng")]
    public List<decimal> Lng { get; set; } // kinh độ
}