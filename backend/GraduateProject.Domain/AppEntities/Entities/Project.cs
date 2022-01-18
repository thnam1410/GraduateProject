using GraduateProject.Domain.Common;
using GraduateProject.Domain.Constants;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Project : TrackableEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Area { get; set; }
    public string Price { get; set; }
    public ApproveStatus ApproveStatus { get; set; }
    public string Active { get; set; }

    public ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}