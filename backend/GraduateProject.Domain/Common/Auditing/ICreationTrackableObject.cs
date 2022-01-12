namespace GraduateProject.Domain.Common.Auditing;

public interface ICreationTrackableObject: IHasCreationTime
{
    string CreatedBy { get; set; }
}