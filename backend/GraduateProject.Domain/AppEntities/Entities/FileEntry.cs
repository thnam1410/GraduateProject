using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class FileEntry: IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public DateTime UploadTime { get; set; }
    public string FileName { get; set; }
    public string FileLocation { get; set; }
}