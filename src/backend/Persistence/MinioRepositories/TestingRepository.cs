using System.Text.Json;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Microsoft.Extensions.Logging;

namespace Persistence.MinioRepositories
{
    public class TestingRepository : ITestingRepository
    {
        private readonly IMinioClient _minioClient;
        private readonly ILogger<TestingRepository> _logger;
        private const string BucketName = "barsdb";

        public TestingRepository(IMinioClient minioClient, ILogger<TestingRepository> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<List<TestingQuestion>>> GetTestAsync(int courseId, int moduleId, CancellationToken cancellationToken = default)
        {
            var objectName = $"courses/{courseId}/modules/{moduleId}/Test.json";
            _logger.LogInformation("GetTestAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {
                using var memoryStream = new MemoryStream();

                await _minioClient.GetObjectAsync(new GetObjectArgs()
                        .WithBucket(BucketName)
                        .WithObject(objectName)
                        .WithCallbackStream(stream => stream.CopyTo(memoryStream)),
                    cancellationToken);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var test = await JsonSerializer.DeserializeAsync<List<TestingQuestion>>(memoryStream, cancellationToken: cancellationToken);

                if (test is null)
                {
                    _logger.LogWarning("Failed to deserialize test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                    return Result<List<TestingQuestion>>.Failure("Не удалось десериализовать тест.");
                }

                _logger.LogInformation("Successfully retrieved and deserialized test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result<List<TestingQuestion>>.Success(test);
            }
            catch (ObjectNotFoundException)
            {
                _logger.LogWarning("Test file not found in MinIO for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result<List<TestingQuestion>>.Failure("Файл теста не найден в MinIO.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result<List<TestingQuestion>>.Failure($"Произошла ошибка при получении теста: {ex.Message}");
            }
        }

        public async Task<Result<List<int>>> GetCorrectAnswersAsync(int courseId, int moduleId, CancellationToken cancellationToken = default)
        {
            var objectName = $"courses/{courseId}/modules/{moduleId}/Test.json";
            _logger.LogInformation("GetCorrectAnswersAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {
                using var memoryStream = new MemoryStream();

                await _minioClient.GetObjectAsync(new GetObjectArgs()
                        .WithBucket(BucketName)
                        .WithObject(objectName)
                        .WithCallbackStream(stream => stream.CopyTo(memoryStream)),
                    cancellationToken);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var test = await JsonSerializer.DeserializeAsync<List<TestingQuestion>>(memoryStream, cancellationToken: cancellationToken);

                if (test is null)
                {
                    _logger.LogWarning("Failed to deserialize test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                    return Result<List<int>>.Failure("Не удалось десериализовать тест.");
                }

                var correctAnswers = test.Select(q => q.CorrectAnswer).ToList();
                _logger.LogInformation("Successfully retrieved correct answers for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

                return Result<List<int>>.Success(correctAnswers);
            }
            catch (ObjectNotFoundException)
            {
                _logger.LogWarning("Test file not found in MinIO for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result<List<int>>.Failure("Файл теста не найден в MinIO.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving correct answers for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result<List<int>>.Failure($"Произошла ошибка при получении теста: {ex.Message}");
            }
        }

        public async Task<Result> AddOrUpdateTestAsync(int courseId, int moduleId, List<TestingQuestion> testQuestions, CancellationToken cancellationToken = default)
        {
            var objectName = $"courses/{courseId}/modules/{moduleId}/Test.json";
            _logger.LogInformation("AddOrUpdateTestAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {
                var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(BucketName), cancellationToken);

                if (!bucketExists)
                {
                    _logger.LogInformation("Bucket does not exist, creating bucket: {BucketName}", BucketName);
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketName), cancellationToken);
                }

                var json = JsonSerializer.Serialize(testQuestions);

                using var memoryStream = new MemoryStream();
                var writer = new StreamWriter(memoryStream);
                writer.Write(json);
                writer.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);

                await _minioClient.PutObjectAsync(new PutObjectArgs()
                        .WithBucket(BucketName)
                        .WithObject(objectName)
                        .WithStreamData(memoryStream)
                        .WithObjectSize(memoryStream.Length),
                    cancellationToken);

                _logger.LogInformation("Successfully added or updated test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding or updating test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Failure($"Произошла ошибка при добавлении или обновлении теста: {ex.Message}");
            }
        }
    }
}
