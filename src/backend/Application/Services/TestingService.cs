using Core.Interfaces.Services;
using Core.Utils;

namespace Application.Services;

public class TestingService : ITestingService
{
    public Task<Result<ICollection<string>>> GetQuestionsForTest(int testId)
    {
        throw new NotImplementedException();
    }
}