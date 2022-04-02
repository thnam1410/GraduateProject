using GraduateProject.Application.Core;
using GraduateProject.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[Route("/api/file")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<ApiResponse<ICollection<Guid>>> HandleUploadFiles(ICollection<IFormFile> files)
    {
        try
        {
            return ApiResponse<ICollection<Guid>>.Ok(await _fileService.UploadFilesAsync(files, "Test"));
        }
        catch (Exception e)
        {
            return ApiResponse<ICollection<Guid>>.Fail(e.Message);
        }
    }

    [HttpPost("remove-files")]
    public async Task<ApiResponse> HandleRemoveFiles(List<Guid> files)
    {
        try
        {
            await _fileService.RemoveFilesAsync(files);
            return ApiResponse.Ok();
        }
        catch (Exception e)
        {
            return ApiResponse.Fail(e.Message);
        }
    }
}