using GraduateProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GraduateProject.Infrastructure;

public class AppUnitOfWork: UnitOfWork
{
    public AppUnitOfWork(AppDbContext context, IServiceProvider servicesProvider) : base(context, servicesProvider)
    {
    }
}