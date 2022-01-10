using Microsoft.AspNetCore.Identity;

namespace GraduateProject.Domain.Ums.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public UserAccount UserAccount { get; set; }
    public Role Role { get; set; }
}