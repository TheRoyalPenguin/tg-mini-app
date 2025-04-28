using System.Text.Json;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Minio.Exceptions;
using Microsoft.Extensions.Logging;
using Core.Interfaces.Services;

namespace Persistence.MinioRepositories
{
    public class TestingRepository : ITestingRepository
    {
        private readonly IStorageService _storage;
        private readonly ILogger<TestingRepository> _logger;

        public TestingRepository(IStorageService storage, ILogger<TestingRepository> logger)
        {
            _storage = storage;
            _logger = logger;
        }
        public async Task<Result<List<TestingQuestion>>> GetTestAsync(int courseId, int moduleId, CancellationToken cancellationToken = default)
        {
            var key = GetPath(courseId, moduleId);
            _logger.LogInformation("GetTestAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {
                using var memoryStream = new MemoryStream();

                await _storage.DownloadAsync(memoryStream, key, cancellationToken);

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
            _logger.LogInformation("GetCorrectAnswersAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {

                var test = await GetTestAsync(courseId, moduleId, cancellationToken);

                if (!test.IsSuccess || test.Data == null)
                {
                    _logger.LogWarning("Failed to deserialize test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                    return Result<List<int>>.Failure(test.ErrorMessage);
                }

                var correctAnswers = test.Data.Select(q => q.CorrectAnswer).ToList();
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
            var key = GetPath(courseId, moduleId);
            _logger.LogInformation("AddOrUpdateTestAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {
                var json = JsonSerializer.Serialize(testQuestions);

                using var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
                await _storage.UploadAsync(memoryStream, key, cancellationToken);

                _logger.LogInformation("Successfully added or updated test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding or updating test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Failure($"Произошла ошибка при добавлении или обновлении теста: {ex.Message}");
            }
        }
        
        public async Task<Result> DeleteTestAsync(int courseId, int moduleId, CancellationToken cancellationToken = default)
        {
            var key = GetPath(courseId, moduleId);
            _logger.LogInformation("DeleteTestAsync called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            try
            {
                await _storage.DeleteAsync(key, cancellationToken);

                _logger.LogInformation("Successfully deleted test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Success();
            }
            catch (ObjectNotFoundException)
            {
                _logger.LogWarning("Test file not found in MinIO for deletion. courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Failure("Файл теста не найден для удаления.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Result.Failure($"Произошла ошибка при удалении теста: {ex.Message}");
            }
        }

        private static string GetPath(int courseId, int moduleId)
        {
            return $"courses/{courseId}/modules/{moduleId}/Test.json";
        }
    }
}
