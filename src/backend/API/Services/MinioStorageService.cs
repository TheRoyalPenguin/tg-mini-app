using API.Configurations;
using Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace API.Services;

public class MinioStorageService : IStorageService
{
    private readonly IMinioClient _client;
    private readonly IMinioClient _publicClient;
    private readonly string _bucket;
    private readonly ILogger<MinioStorageService> _logger;

    public MinioStorageService(IOptions<MinioSettings> options, ILogger<MinioStorageService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var settings = options.Value;

        var endpoint = settings.Endpoint;
        var publicEndpoint = settings.PublicEndpoint;
        var accessKey = settings.AccessKey;
        var secretKey = settings.SecretKey;
        _bucket = settings.Bucket;

        _client = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();

        _publicClient = new MinioClient()
            .WithEndpoint(publicEndpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();
    }


    public async Task UploadAsync(Stream data, string key, CancellationToken ct = default)
    {
        _logger.LogDebug("Starting upload for " + key);

        try
        {
            var existsArgs = new BucketExistsArgs().WithBucket(_bucket);
            if (!await _client.BucketExistsAsync(existsArgs, ct))
            {
                _logger.LogInformation("Creating bucket " + _bucket);
                var makeArgs = new MakeBucketArgs().WithBucket(_bucket);
                await _client.MakeBucketAsync(makeArgs, ct);
            }

            if (!data.CanRead)
            {
                _logger.LogError("Upload failed: stream is not readable");
                throw new InvalidOperationException("Stream is not readable");
            }

            if (data.CanSeek)
            {
                data.Position = 0;
            }

            if (data.CanSeek && data.Length == 0)
            {
                _logger.LogWarning("Empty stream detected for " + key);
                throw new InvalidOperationException("Cannot upload empty file to MinIO");
            }

            _logger.LogDebug("Uploading " + key + "(" + data.Length + " bytes)");

            var putArgs = new PutObjectArgs()
                .WithBucket(_bucket)
                .WithObject(key)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType("application/octet-stream");

            await _client.PutObjectAsync(putArgs, ct);

            _logger.LogInformation("Successfully uploaded " + key);
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "MinIO error uploading " + key + ": " + ex.Message);
            throw new IOException("MinIO upload failed", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error uploading " + key);
            throw;
        }
    }

    public async Task DownloadAsync(Stream outputStream, string key, CancellationToken ct = default)
    {
        _logger.LogDebug("Starting download for " + key);
        await _client.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(_bucket)
                .WithObject(key)
                .WithCallbackStream(s => s.CopyTo(outputStream)),
            ct);
        if (outputStream.CanSeek)
            outputStream.Position = 0;
        _logger.LogInformation("Downloaded " + key);
    }

    public async Task DeleteAsync(string key, CancellationToken ct = default)
    {
        _logger.LogDebug("Deleting object " + key);
        await _client.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(_bucket)
                .WithObject(key),
            ct);
        _logger.LogInformation("Deleted " + key);
    }

    public async Task<string> GetPresignedUrlAsync(string key)
    {
        _logger.LogDebug("Generating presigned URL for " + key);

        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(_bucket)
                .WithObject(key)
                .WithExpiry(3600);

            string url = await _publicClient.PresignedGetObjectAsync(args);

            _logger.LogDebug("Successfully generated URL for " + key);
            return url;
        }
        catch (MinioException ex)
        {
            _logger.LogWarning("Object " + key + " not found");
            throw new FileNotFoundException("Object " + key + " not found in MinIO", ex);
        }
    }
}