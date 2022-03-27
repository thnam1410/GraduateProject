using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class OfferPackage : TrackableEntity<Guid>
{
    public string Name { get; set; }
    public int Order { get; set; }
    public decimal Price { get; set; }
    public int ActiveDay { get; set; }
    public bool Active { get; set; }
    // public ICollection<Post> Posts { get; set; } = new List<Post>();
}