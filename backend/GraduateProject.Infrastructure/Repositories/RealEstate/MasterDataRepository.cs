using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Infrastructure.Repositories.RealEstate;

public class MasterDataRepository: EfRepository<MasterData, Guid>, IMasterDataRepository
{
    public MasterDataRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}