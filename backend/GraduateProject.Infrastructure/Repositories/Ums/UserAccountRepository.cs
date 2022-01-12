using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Ums;

public class UserAccountRepository : EfRepository<UserAccount, Guid>, IUserAccountRepository
{
    public UserAccountRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserAccount> GetUserWithAuthInfo(Guid id)
    {
        return await _dbSet.AsQueryable().AsNoTracking()
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}