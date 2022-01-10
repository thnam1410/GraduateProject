using GraduateProject.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace GraduateProject.Domain.Ums.Entities;

public class Role: IdentityRole<Guid>, IEntity<Guid>
{
    public string Code { get; set; }
    public string DisplayName { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}