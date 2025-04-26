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
    private readonly IModuleAccessRepository moduleAccessRepository;
    public TestingService(ICourseRepository courseRepository, IModuleRepository moduleRepository, ITestingRepository testingRepository, IModuleAccessRepository moduleAccessRepository)
    {
        this.courseRepository = courseRepository;
        this.moduleRepository = moduleRepository;
        this.testingRepository = testingRepository;
        this.moduleAccessRepository = moduleAccessRepository;
    }
    public async Task<Result<List<TestingQuestion>>> GetQuestionsForTest(
        int courseId, 
        int moduleId,
        int userId)
    {
        /*var courseExistsResult = await courseRepository.ExistsAsync(courseId);
        if (!courseExistsResult.IsSuccess || !courseExistsResult.Data)
            return Result<List<TestingQuestion>>.Failure($"Курс с ID {courseId} не найден.");
        
        var moduleExistsResult = await moduleRepository.ExistsForCourseAsync(courseId, moduleId);
        if (!moduleExistsResult.IsSuccess || !moduleExistsResult.Data)
            return Result<List<TestingQuestion>>.Failure($"Модуль с ID {moduleId} не найден в курсе.");*/
        
        var moduleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(userId, moduleId);
        if (moduleAccess == null || !moduleAccess.IsModuleAvailable)
            return Result<List<TestingQuestion>>.Failure("Модуль недоступен.");
        
        var module = await moduleRepository.GetByIdAsync(moduleId);
        if (module == null)
            return Result<List<TestingQuestion>>.Failure("Модуль не найден.");

        int completedLongreads = moduleAccess.LongreadCompletions?.Count ?? 0;
        if (completedLongreads < module.Data.LongreadCont)
            return Result<List<TestingQuestion>>.Failure("Доступ к тесту закрыт. Прочитайте все лонгриды.");
        
        var testResult = await testingRepository.GetTestAsync(courseId, moduleId);
        if (!testResult.IsSuccess || testResult.Data == null)
            return Result<List<TestingQuestion>>.Failure("Тест не найден.");

       return Result<List<TestingQuestion>>.Success(testResult.Data);
    }

    public async Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand command)
    {
        // Заглушка: случайная генерация результатов
        var rnd = new Random();
        var correctPercentage = rnd.Next(0, 101);
        var isSuccess = correctPercentage >= 75;
    
        // Получаем доступ к модулю для данного пользователя
        var moduleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(command.UserId, command.ModuleId);
        if (moduleAccess == null)
            return Result<SubmitAnswersResult>.Failure("Доступ к модулю не найден");

        // Обработка успешной попытки
        if (isSuccess)
        {
            moduleAccess.IsModuleCompleted = true;
            moduleAccess.CompletionDate = DateOnly.FromDateTime(DateTime.UtcNow);
        }
        else // Обработка неудачной попытки
        {
            moduleAccess.TestTriesCount++;
    
            if (moduleAccess.TestTriesCount >= 3)
            {
                moduleAccess.TestTriesCount = 0;
                moduleAccess.LongreadCompletions.Clear();
            }
        }

        // Обновляем данные в базе данных
        var updateResult = await moduleAccessRepository.UpdateAsync(moduleAccess);
        if (!updateResult.IsSuccess)
            return Result<SubmitAnswersResult>.Failure(updateResult.ErrorMessage);

        var result = new SubmitAnswersResult(
            isSuccess,
            command.Answers.Count,
            (int)(command.Answers.Count * correctPercentage / 100),
            command.Answers.Select(a => rnd.NextDouble() < 0.75).ToList()
        );

        return Result<SubmitAnswersResult>.Success(result);
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