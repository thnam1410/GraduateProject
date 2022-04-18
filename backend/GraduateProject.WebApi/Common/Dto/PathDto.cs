using Newtonsoft.Json;

namespace GraduateProject.Common.Dto;

public class PathDto
{
    [JsonProperty(PropertyName = "lat")]
    public List<decimal> Lat { get; set; }
    [JsonProperty(PropertyName = "lng")]
    public List<decimal> Lng { get; set; }
}