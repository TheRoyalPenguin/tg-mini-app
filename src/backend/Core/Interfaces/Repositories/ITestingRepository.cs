using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface ITestingRepository
{
    Task<Result<List<TestingQuestion>>> GetTestAsync(int courseId, int moduleId, CancellationToken cancellationToken = default);

    Task<Result> AddOrUpdateTestAsync(int courseId, int moduleId, List<TestingQuestion> testQuestions,
        CancellationToken cancellationToken = default);

    Task<Result<List<int>>> GetCorrectAnswersAsync(int courseId, int moduleId,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteTestAsync(int courseId, int moduleId, CancellationToken cancellationToken = default);
}