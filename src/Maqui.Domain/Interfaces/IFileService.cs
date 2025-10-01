namespace Maqui.Domain.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(byte[] fileBytes, string fileName, string folder);
    Task DeleteFileAsync(string filePath);
    Task<bool> ValidateFileSizeAsync(long fileSize, int maxSizeMB);
    Task<bool> ValidateFileExtensionAsync(string fileName, string[] allowedExtensions);
}