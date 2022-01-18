using System.Collections.ObjectModel;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class SalePersonInfo : TrackableEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phonenumber1 { get; set; }
    public string? Phonenumber2 { get; set; }
    public string Email { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}