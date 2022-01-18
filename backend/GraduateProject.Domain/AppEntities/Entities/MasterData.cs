using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class MasterData: Entity<Guid>
{
    public Guid? ParentId { get; set; }
    public string MasterKey { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    
    public virtual MasterData ParentMasterData { get; set; }
    public virtual ICollection<MasterData> Children { get; set; }
}