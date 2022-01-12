namespace GraduateProject.Domain.Common.Auditing;

public interface IHasCreationTime
{
    DateTime? CreatedTime { get; set; }
}