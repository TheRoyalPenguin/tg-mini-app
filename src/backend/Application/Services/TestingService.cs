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
        var resources = await repository.GetAllByModuleIdAsync(moduleId);
        if (!resources.IsSuccess)
        {
            return Result<List<TestingQuestion>>.Failure(resources.ErrorMessage);
        }

        var testResource = resources.Data
            .FirstOrDefault(r => r.Type == ResourceType.Test && !string.IsNullOrEmpty(r.JsonUri));

        if (testResource == null)
        {
            return Result<List<TestingQuestion>>.Failure("Не найден ресурс типа 'тест' для данного модуля");
        }
        
        //TODO получения json тестов из минио
        throw new Exception();
    }

    public async Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand answers)
    {
        throw new NotImplementedException();
    }
}