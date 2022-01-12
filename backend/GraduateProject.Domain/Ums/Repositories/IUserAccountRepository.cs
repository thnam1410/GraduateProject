using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;

namespace GraduateProject.Domain.Ums.Repositories;

public interface IUserAccountRepository : IRepository<UserAccount, Guid>
{
    Task<UserAccount> GetUserWithAuthInfo(Guid id);

}