using System.Diagnostics.CodeAnalysis;

namespace GraduateProject.Domain.Common;

public interface ITrackableEntity<TUserKey>
{
    public DateTime? CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public string? LastModifiedBy { get; set; }
}