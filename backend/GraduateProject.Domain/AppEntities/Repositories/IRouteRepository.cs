using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Repositories;

public interface IRouteRepository: IRepository<Route, int>
{
    Task UpdateIdentityInsert(bool isOn);
}