using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;

namespace GraduateProject.Application.Core;

public interface IFileService
{
    Task<ICollection<Guid>> UploadFilesAsync(ICollection<IFormFile> files, string uploadDirectory);

    Task RemoveFilesAsync(ICollection<Guid> removeFileEntryIds);

}