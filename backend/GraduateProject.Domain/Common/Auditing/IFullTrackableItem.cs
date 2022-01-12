namespace GraduateProject.Domain.Common.Auditing;

public interface IFullTrackableItem : 
    ICreationTrackableObject,
    IHasCreationTime,
    IModificationTrackableObject,
    IHasModificationTime
{
}