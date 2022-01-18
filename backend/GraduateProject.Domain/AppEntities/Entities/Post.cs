using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;

namespace GraduateProject.Domain.AppEntities.Entities;

public class Post: TrackableEntity<Guid>
{
    public Guid UserAccountId { get; set; }
    public Guid OfferPackageId { get; set; }
    public Guid SalePersonInfoId { get; set; }
    public string Status { get; set; }
    public bool Active { get; set; }
    public DateTime PostStartDate { get; set; }
    public bool IsUseFreeDayConfig { get; set; }
    public int PriorityOrderRank { get; set; }
    
    public UserAccount UserAccount { get; set; }
    public OfferPackage OfferPackage { get; set; }
    public SalePersonInfo SalePersonInfo { get; set; }
}