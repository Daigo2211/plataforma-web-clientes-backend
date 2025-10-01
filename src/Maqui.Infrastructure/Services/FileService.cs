using Microsoft.Extensions.Configuration;
using Maqui.Domain.Interfaces;

namespace Maqui.Infrastructure.Services;

public sealed class FileService : IFileService
{
    private readonly string _uploadBasePath;
    private readonly int _maxFileSizeMB;

    public FileService(IConfiguration configuration)
    {
        _uploadBasePath = configuration["FileSettings:UploadBasePath"] ?? "wwwroot/uploads";
        _maxFileSizeMB = int.Parse(configuration["FileSettings:MaxFileSizeMB"] ?? "5");
    }

    public async Task<string> SaveFileAsync(byte[] fileBytes, string fileName, string folder)
    {
        var folderPath = Path.Combine(_uploadBasePath, folder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        await File.WriteAllBytesAsync(filePath, fileBytes);

        return "/" + Path.Combine("uploads", folder, uniqueFileName).Replace("\\", "/");
    }

    public Task DeleteFileAsync(string filePath)
    {
        var cleanPath = filePath.TrimStart('/');
        var fullPath = Path.Combine("wwwroot", cleanPath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }

    public Task<bool> ValidateFileSizeAsync(long fileSize, int maxSizeMB)
    {
        var maxSizeBytes = maxSizeMB * 1024 * 1024;
        return Task.FromResult(fileSize <= maxSizeBytes);
    }

    public Task<bool> ValidateFileExtensionAsync(string fileName, string[] allowedExtensions)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return Task.FromResult(allowedExtensions.Contains(extension));
    }
}