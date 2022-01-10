using GraduateProject.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace GraduateProject.Domain.Ums.Entities;

public class UserAccount : IdentityUser<Guid>, ITrackableEntity<Guid>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    
    public DateTime? CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}