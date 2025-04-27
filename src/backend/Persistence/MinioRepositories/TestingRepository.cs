using System.Text.Json;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Persistence.MinioRepositories;

public class TestingRepository : ITestingRepository
{
    private readonly IMinioClient _minioClient;
    private const string BucketName = "bars"; 

    public TestingRepository(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<Result<List<TestingQuestion>>> GetTestAsync(int courseId, int moduleId, CancellationToken cancellationToken = default)
    {
        var objectName = $"courses/{courseId}/modules/{moduleId}/Test.json";

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
                return Result<List<TestingQuestion>>.Failure("Не удалось десериализовать тест.");

            return Result<List<TestingQuestion>>.Success(test);
        }
        catch (ObjectNotFoundException)
        {
            return Result<List<TestingQuestion>>.Failure("Файл теста не найден в MinIO.");
        }
        catch (Exception ex)
        {
            return Result<List<TestingQuestion>>.Failure($"Произошла ошибка при получении теста: {ex.Message}");
        }
    }
    
    public async Task<Result<List<int>>> GetCorrectAnswersAsync(int courseId, int moduleId, CancellationToken cancellationToken = default)
    {
        var objectName = $"courses/{courseId}/modules/{moduleId}/Test.json";

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
                return Result<List<int>>.Failure("Не удалось десериализовать тест.");

            var correctAnswers = test.Select(q => q.CorrectAnswer).ToList();

            return Result<List<int>>.Success(correctAnswers);
        }
        catch (ObjectNotFoundException)
        {
            return Result<List<int>>.Failure("Файл теста не найден в MinIO.");
        }
        catch (Exception ex)
        {
            return Result<List<int>>.Failure($"Произошла ошибка при получении теста: {ex.Message}");
        }
    }

    
    public async Task<Result> AddOrUpdateTestAsync(int courseId, int moduleId, List<TestingQuestion> testQuestions, CancellationToken cancellationToken = default)
    {
        var objectName = $"courses/{courseId}/modules/{moduleId}/Test.json";
        try
        {
            // Сериализуем вопросы в формат JSON
            var json = JsonSerializer.Serialize(testQuestions);

            using var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(json);
            writer.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Загружаем или обновляем файл в MinIO
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(objectName)
                    .WithStreamData(memoryStream)
                    .WithObjectSize(memoryStream.Length),
                cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Произошла ошибка при добавлении или обновлении теста: {ex.Message}");
        }
    }
}