using GraduateProject.Domain.AppEntities.Entities;

namespace GraduateProject.Application.RealEstate.RouteDto;

public class InfoRouteSearchViewDto
{
    public string Date { get; set; }
    public List<InfoRouteSearch> InfoRouteSearchList { get; set; }

}