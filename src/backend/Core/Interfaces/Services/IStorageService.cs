namespace Core.Interfaces.Services;

public interface IStorageService
{
    Task UploadAsync(Stream data, string key, CancellationToken ct = default);
    Task DownloadAsync(Stream outputStream, string key, CancellationToken ct = default);
    Task DeleteAsync(string key, CancellationToken ct = default);
    Task<string> GetPresignedUrlAsync(string key);
}
