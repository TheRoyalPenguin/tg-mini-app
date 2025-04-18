using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITestingService
{
    Task<Result<ICollection<string>>> GetQuestionsForTest(int testId);
}