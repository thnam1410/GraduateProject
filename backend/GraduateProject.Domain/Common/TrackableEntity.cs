namespace GraduateProject.Domain.Common;

public class TrackableEntity<TKey> : Entity<TKey>, ITrackableEntity<TKey>
{
    public DateTime? CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}