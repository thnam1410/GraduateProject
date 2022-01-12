using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;

namespace GraduateProject.Domain.Ums.Repositories;

public interface IRoleRepository: IRepository<Role, Guid>
{
    Task<Role> GetUserRole();
    Task AddUserRole(Guid newUserId, Guid userRoleId);
}