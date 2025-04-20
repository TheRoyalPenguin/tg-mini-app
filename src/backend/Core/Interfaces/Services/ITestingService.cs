using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITestingService
{
    Task<Result<List<TestingQuestion>>> GetQuestionsForTest(int moduleId);
    Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand dto);
}