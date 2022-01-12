namespace GraduateProject.Domain.Common.Auditing;

public interface IModificationTrackableObject: IHasModificationTime
{
    string LastModifiedBy { get; set; }
}