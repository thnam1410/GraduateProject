namespace GraduateProject.Domain.Common.Auditing;

public interface IHasModificationTime
{
    DateTime? LastModifiedTime { get; set; }
}