using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;

namespace GraduateProject.Domain.AppEntities.Entities;

public class TopUpHistory: Entity<Guid>
{
    public long Amount { get; set; }
    public Guid UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; }
}