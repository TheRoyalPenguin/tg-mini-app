using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class TestingService : ITestingService
{
    private readonly ICourseRepository courseRepository;
    private readonly IModuleRepository moduleRepository;
    private readonly ITestingRepository testingRepository;
    public TestingService(ICourseRepository courseRepository, IModuleRepository moduleRepository, ITestingRepository testingRepository)
    {
        this.courseRepository = courseRepository;
        this.moduleRepository = moduleRepository;
        this.testingRepository = testingRepository;
    }
    public async Task<Result<List<TestingQuestion>>> GetQuestionsForTest(int courseId, int moduleId)
    {
        var courseExistsResult = await courseRepository.ExistsAsync(courseId);
        if (!courseExistsResult.IsSuccess || !courseExistsResult.Data)
            return Result<List<TestingQuestion>>.Failure($"Курс с ID {courseId} не найден.");
        
        var moduleExistsResult = await moduleRepository.ExistsForCourseAsync(courseId, moduleId);
        if (!moduleExistsResult.IsSuccess || !moduleExistsResult.Data)
            return Result<List<TestingQuestion>>.Failure($"Модуль с ID {moduleId} не найден в курсе с ID {courseId}.");
        
        var testResult = await testingRepository.GetTestAsync(courseId, moduleId);
        if (!testResult.IsSuccess || testResult.Data == null)
            return Result<List<TestingQuestion>>.Failure("Тест не найден.");

        return Result<List<TestingQuestion>>.Success(testResult.Data);
    }

    public async Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand answers)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Result> AddOrUpdateTestAsync(int courseId, int moduleId, List<TestingQuestion> testQuestions)
    {
        var courseExistsResult = await courseRepository.ExistsAsync(courseId);
        if (!courseExistsResult.IsSuccess || !courseExistsResult.Data)
            return Result.Failure($"Курс с ID {courseId} не найден.");
        
        var moduleExistsResult = await moduleRepository.ExistsForCourseAsync(courseId, moduleId);
        if (!moduleExistsResult.IsSuccess || !moduleExistsResult.Data)
            return Result.Failure($"Модуль с ID {moduleId} не найден в курсе с ID {courseId}.");

        try
        {
            var addOrUpdateResult = await testingRepository.AddOrUpdateTestAsync(courseId, moduleId, testQuestions);

            if (addOrUpdateResult.IsSuccess)
            {
                return Result.Success();
            }

            return Result.Failure("Не удалось добавить или обновить тест.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Произошла ошибка при добавлении или обновлении теста: {ex.Message}");
        }
    }
}