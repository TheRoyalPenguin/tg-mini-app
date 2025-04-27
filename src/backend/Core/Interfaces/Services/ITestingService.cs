using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITestingService
{
    Task<Result<List<TestingQuestion>>> GetQuestionsForTest(int moduleId, int courseId, int userId);
    Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand dto);

    Task<Result> AddOrUpdateTestAsync(int courseId, int moduleId, List<TestingQuestion> testQuestions);
    Task<Result> DeleteTest(int courseId, int moduleId);
}