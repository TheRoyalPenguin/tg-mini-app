using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class TestingService : ITestingService
{
    private readonly IResourceRepository repository;
    public TestingService(IResourceRepository resourceRepository)
    {
        repository = resourceRepository;
    }
    public async Task<Result<List<TestingQuestion>>> GetQuestionsForTest(int moduleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand answers)
    {
        throw new NotImplementedException();
    }
}