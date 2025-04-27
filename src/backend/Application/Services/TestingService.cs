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
        
        /*var moduleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(userId, moduleId);
        if (moduleAccess == null || !moduleAccess.IsModuleAvailable)
            return Result<List<TestingQuestion>>.Failure("Модуль недоступен.");
        
        var module = await moduleRepository.GetByIdAsync(moduleId);
        if (module == null)
            return Result<List<TestingQuestion>>.Failure("Модуль не найден.");

        int completedLongreads = moduleAccess.LongreadCompletions?.Count ?? 0;
        if (completedLongreads < module.Data.LongreadCount)
            return Result<List<TestingQuestion>>.Failure("Доступ к тесту закрыт. Прочитайте все лонгриды.");*/
        
        var testResult = await testingRepository.GetTestAsync(courseId, moduleId);
        if (!testResult.IsSuccess || testResult.Data == null)
            return Result<List<TestingQuestion>>.Failure("Тест не найден.");

       return Result<List<TestingQuestion>>.Success(testResult.Data);
    }
    public async Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand command)
    {
        // Получаем правильные ответы из MinIO
        var correctAnswersResult = await testingRepository.GetCorrectAnswersAsync(command.CourseId, command.ModuleId);
        if (!correctAnswersResult.IsSuccess)
            return Result<SubmitAnswersResult>.Failure(correctAnswersResult.ErrorMessage);

        var correctAnswers = correctAnswersResult.Data;

        // Проверяем, что количество ответов совпадает
        if (command.Answers.Count != correctAnswers.Count)
            return Result<SubmitAnswersResult>.Failure("Количество ответов не соответствует количеству вопросов в тесте.");

        // Сравниваем ответы
        int correctCount = 0;
        var correctness = new List<bool>();

        for (int i = 0; i < correctAnswers.Count; i++)
        {
            bool isCorrect = command.Answers[i] == correctAnswers[i];
            if (isCorrect)
                correctCount++;
            correctness.Add(isCorrect);
        }

        double correctPercentage = (double)correctCount / correctAnswers.Count * 100;
        bool isSuccess = correctPercentage >= 75;

        // Получаем доступ к модулю для данного пользователя
        var moduleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(command.UserId, command.ModuleId);
        if (moduleAccess == null)
            return Result<SubmitAnswersResult>.Failure("Доступ к модулю не найден.");

        // Обновляем прогресс пользователя в зависимости от успеха
        if (isSuccess)
        {
            moduleAccess.IsModuleCompleted = true;
            moduleAccess.CompletionDate = DateOnly.FromDateTime(DateTime.UtcNow);

            // Новый код: ищем следующий модуль
            var nextModule = await moduleRepository.GetNextModuleInCourseAsync(command.CourseId, command.ModuleId);
            if (nextModule != null)
            {
                // Ищем доступ пользователя к следующему модулю
                var nextModuleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(command.UserId, nextModule.Data.Id);

                if (nextModuleAccess != null)
                {
                    // Если доступ найден, обновляем доступность
                    nextModuleAccess.IsModuleAvailable = true;
                    var nextModuleUpdateResult = await moduleAccessRepository.UpdateAsync(nextModuleAccess);

                    if (!nextModuleUpdateResult.IsSuccess)
                        return Result<SubmitAnswersResult>.Failure("Не удалось обновить доступ к следующему модулю.");
                }
            }
        }
        else
        {
            moduleAccess.TestTriesCount++;

            if (moduleAccess.TestTriesCount >= 3)
            {
                moduleAccess.TestTriesCount = 0;
                moduleAccess.LongreadCompletions.Clear();
            }
        }

        // Сохраняем изменения
        var updateResult = await moduleAccessRepository.UpdateAsync(moduleAccess);
        if (!updateResult.IsSuccess)
            return Result<SubmitAnswersResult>.Failure(updateResult.ErrorMessage);

        // Формируем результат
        var result = new SubmitAnswersResult(
            isSuccess,
            command.Answers.Count,
            correctCount,
            correctness
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