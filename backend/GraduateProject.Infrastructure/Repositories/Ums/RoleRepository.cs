using GraduateProject.Domain.Common;
using GraduateProject.Domain.Constants;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.Ums;

public class RoleRepository : EfRepository<Role, Guid>, IRoleRepository
{
    private readonly DbSet<UserRole> _dbSetUserRole;
    private readonly AppDbContext _testDbContext;

    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSetUserRole = dbContext.Set<UserRole>();
        _testDbContext = dbContext;
    }

    public async Task<Role> GetUserRole()
    {
        return await _dbSet.AsQueryable().FirstOrDefaultAsync(x => x.Code == RoleConstants.USER_ROLE);
    }

    public async Task AddUserRole(Guid newUserId, Guid userRoleId)
    {
        var newEntity = new UserRole() {RoleId = userRoleId, UserId = newUserId};
        await _dbContext.AddAsync(newEntity);
        // await _dbContext.SaveChangesAsync();
    }
}