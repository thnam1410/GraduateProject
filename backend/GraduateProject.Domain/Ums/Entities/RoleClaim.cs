using Microsoft.AspNetCore.Identity;

namespace GraduateProject.Domain.Ums.Entities;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role Role { get; set; }
}