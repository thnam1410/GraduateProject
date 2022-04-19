using System.Collections.ObjectModel;
using GraduateProject.Domain.AppEntities.Entities;
using GraduateProject.Domain.AppEntities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;

namespace GraduateProject.Application.Core;

public class FileService: IFileService
{
    private readonly IFileRepository _fileRepository;
    private const string UploadFileFolder = "UploadFiles";
    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<ICollection<Guid>> UploadFilesAsync(ICollection<IFormFile> files, string uploadDirectory)
    {
        var fileIds = new List<Guid>();
        foreach (var file in files)
        {
            var fileNameWithOutExtensions = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "_");
            var fileExtension = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadDirectory, $"{fileNameWithOutExtensions}_{Guid.NewGuid().ToString()}{fileExtension}");
            var fileEntity = new FileEntry()
            {
                Name = file.FileName,
                FileName = file.FileName,
                Size = file.Length,
                FileLocation = filePath,
                UploadTime = DateTime.Now,
            };
            var uploadFilePath = Path.Combine(UploadFileFolder, uploadDirectory);
            if (!Directory.Exists(uploadFilePath)) Directory.CreateDirectory(uploadFilePath);
            using (var stream = File.Create(Path.Combine(UploadFileFolder, filePath)))
            {
                await file.CopyToAsync(stream);
            }
            await _fileRepository.AddAsync(fileEntity, true);
            fileIds.Add(fileEntity.Id);
        }
        return fileIds;
    }

    public async Task RemoveFilesAsync(ICollection<Guid> removeFileEntryIds)
    {
        var fileEntries = await _fileRepository.Queryable().Where(x => removeFileEntryIds.Contains(x.Id)).ToListAsync();
        foreach (var fileEntry in fileEntries)
        {
            var path = Path.Combine(UploadFileFolder, fileEntry.FileLocation);
            if (File.Exists(path)) File.Delete(path);
        }
        await _fileRepository.DeleteRangeAsync(fileEntries, true);
    }   
}