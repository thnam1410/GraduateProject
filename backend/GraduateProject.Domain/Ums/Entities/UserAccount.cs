using GraduateProject.Domain.Common;
using GraduateProject.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace GraduateProject.Domain.Ums.Entities;

public class UserAccount : IdentityUser<Guid>, ITrackableEntity<Guid>, IEntity<Guid>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public bool? Active { get; set; } = true;

    public DateTime? CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public string? LastModifiedBy { get; set; }


    public bool HasRoleAdminSystem() => GetRoles().FirstOrDefault(x => x.Code == RoleConstants.ADMIN_ROLE) != null;

    public IEnumerable<Role> GetRoles() => UserRoles.Select(x => x.Role).ToList();
}