using System;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IMasterDataRepository: IRepository<MasterData, Guid>
{
    
}