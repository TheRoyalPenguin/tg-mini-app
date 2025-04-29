using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TestingService(
    ICourseRepository courseRepository,
    IModuleRepository moduleRepository,
    ITestingRepository testingRepository,
    IModuleAccessRepository moduleAccessRepository,
    ILogger<TestingService> logger)
    : ITestingService
{
    private readonly ILogger<TestingService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<Result<List<TestingQuestion>>> GetQuestionsForTest(
        int courseId, 
        int moduleId,
        int userId)
    {
        _logger.LogInformation("Получение вопросов для теста. Курс ID: {CourseId}, Модуль ID: {ModuleId}, Пользователь ID: {UserId}", courseId, moduleId, userId);
        
        var moduleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(userId, moduleId);
        if (moduleAccess == null || !moduleAccess.IsModuleAvailable)
        {
            _logger.LogWarning("Модуль недоступен для пользователя ID {UserId}.", userId);
            return Result<List<TestingQuestion>>.Failure("Модуль недоступен.")!;
        }
        
        var module = await moduleRepository.GetByIdAsync(moduleId);
        if (module.Data == null)
        {
            _logger.LogWarning("Модуль с ID {ModuleId} не найден.", moduleId);
            return Result<List<TestingQuestion>>.Failure("Модуль не найден.")!;
        }

        if (moduleAccess.CompletedLongreadsCount < moduleAccess.ModuleLongreadCount)
        {
            _logger.LogWarning("Доступ к тесту закрыт. Прочитайте все лонгриды для модуля с ID {ModuleId}.", moduleId);
            return Result<List<TestingQuestion>>.Failure("Доступ к тесту закрыт. Прочитайте все лонгриды.")!;
        }
        
        var testResult = await testingRepository.GetTestAsync(courseId, moduleId);
        if (!testResult.IsSuccess || testResult.Data == null)
        {
            _logger.LogWarning("Тест для курса ID {CourseId} и модуля ID {ModuleId} не найден.", courseId, moduleId);
            return Result<List<TestingQuestion>>.Failure("Тест не найден.")!;
        }

        _logger.LogInformation("Успешно получены вопросы для теста. Количество вопросов: {QuestionCount}", testResult.Data.Count);
        return Result<List<TestingQuestion>>.Success(testResult.Data);
    }

    public async Task<Result<SubmitAnswersResult>> SubmitAnswers(SubmitAnswersCommand command)
    {
        _logger.LogInformation("Получение правильных ответов для курса ID: {CourseId}, модуля ID: {ModuleId}", command.CourseId, command.ModuleId);

        // Получаем правильные ответы из MinIO
        var correctAnswersResult = await testingRepository.GetCorrectAnswersAsync(command.CourseId, command.ModuleId);
        if (!correctAnswersResult.IsSuccess)
        {
            _logger.LogWarning("Не удалось получить правильные ответы для курса ID: {CourseId}, модуля ID: {ModuleId}. Ошибка: {ErrorMessage}", command.CourseId, command.ModuleId, correctAnswersResult.ErrorMessage);
            return Result<SubmitAnswersResult>.Failure(correctAnswersResult.ErrorMessage);
        }

        var correctAnswers = correctAnswersResult.Data;

        if (command.Answers.Count != correctAnswers.Count)
        {
            _logger.LogWarning("Количество ответов не совпадает с количеством вопросов для курса ID: {CourseId}, модуля ID: {ModuleId}", command.CourseId, command.ModuleId);
            return Result<SubmitAnswersResult>.Failure("Количество ответов не соответствует количеству вопросов в тесте.");
        }

        int correctCount = 0;
        var correctness = new List<bool>();

        for (int i = 0; i < correctAnswers.Count; i++)
        {
            bool isCorrect = command.Answers[i] == correctAnswers[i];
            if (isCorrect) correctCount++;
            correctness.Add(isCorrect);
        }

        double correctPercentage = (double)correctCount / correctAnswers.Count * 100;
        bool isSuccess = correctPercentage >= 75;

        _logger.LogInformation("Процент правильных ответов: {CorrectPercentage}%", correctPercentage);

        var moduleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(command.UserId, command.ModuleId);
        if (moduleAccess == null)
        {
            _logger.LogWarning("Доступ к модулю для пользователя ID {UserId}, модуля ID {ModuleId} не найден.", command.UserId, command.ModuleId);
            return Result<SubmitAnswersResult>.Failure("Доступ к модулю не найден.");
        }

        if (isSuccess)
        {
            // Тест успешно сдан
            moduleAccess.IsModuleCompleted = true;
            moduleAccess.CompletionDate = DateOnly.FromDateTime(DateTime.UtcNow);

            var nextModuleResult = await moduleRepository.GetNextModuleInCourseAsync(command.CourseId, command.ModuleId);
            if (nextModuleResult != null && nextModuleResult.IsSuccess)
            {
                var nextModule = nextModuleResult.Data;
                var nextModuleAccess = await moduleAccessRepository.GetByUserAndModuleAsync(command.UserId, nextModule.Id);

                if (nextModuleAccess != null)
                {
                    nextModuleAccess.IsModuleAvailable = true;
                    var nextModuleUpdateResult = await moduleAccessRepository.UpdateAsync(nextModuleAccess);

                    if (!nextModuleUpdateResult.IsSuccess)
                    {
                        _logger.LogWarning("Не удалось обновить доступ к следующему модулю для пользователя ID {UserId}, модуля ID {ModuleId}. Ошибка: {ErrorMessage}", command.UserId, nextModule.Id, nextModuleUpdateResult.ErrorMessage);
                        return Result<SubmitAnswersResult>.Failure("Не удалось обновить доступ к следующему модулю.");
                    }
                }
            }
        }
        else
        {
            // Тест не сдан
            moduleAccess.TestTriesCount++;

            _logger.LogInformation("Попытка {TestTriesCount} для пользователя ID {UserId}, модуля ID {ModuleId}", moduleAccess.TestTriesCount, command.UserId, command.ModuleId);

            if (moduleAccess.TestTriesCount >= 3)
            {
                _logger.LogWarning("Пользователь ID {UserId} исчерпал 3 попытки на модуль ID {ModuleId}. Удаление прочитанных лонгридов.", command.UserId, command.ModuleId);

                moduleAccess.TestTriesCount = 0;
                moduleAccess.LongreadCompletions.Clear(); // Очищаем прочитанные лонгриды
            }
        }
        var updateResult = await moduleAccessRepository.UpdateAsync(moduleAccess);
        if (!updateResult.IsSuccess)
        {
            _logger.LogWarning("Не удалось обновить данные модуля для пользователя ID {UserId}, модуля ID {ModuleId}. Ошибка: {ErrorMessage}", command.UserId, command.ModuleId, updateResult.ErrorMessage);
            return Result<SubmitAnswersResult>.Failure(updateResult.ErrorMessage);
        }

        var result = new SubmitAnswersResult(
            isSuccess,
            command.Answers.Count,
            correctCount,
            correctness
        );

        _logger.LogInformation("Результат сдачи теста: {IsSuccess}. Пользователь ID {UserId}, модуль ID {ModuleId}.", isSuccess, command.UserId, command.ModuleId);
        return Result<SubmitAnswersResult>.Success(result);
    }
    
    public async Task<Result> AddOrUpdateTestAsync(int courseId, int moduleId, List<TestingQuestion> testQuestions)
    {
        _logger.LogInformation("Добавление или обновление теста для курса ID {CourseId}, модуля ID {ModuleId}", courseId, moduleId);

        var courseExistsResult = await courseRepository.ExistsAsync(courseId);
        if (!courseExistsResult.IsSuccess || !courseExistsResult.Data)
        {
            _logger.LogWarning("Курс с ID {CourseId} не найден.", courseId);
            return Result.Failure($"Курс с ID {courseId} не найден.");
        }
        
        var moduleExistsResult = await moduleRepository.ExistsForCourseAsync(courseId, moduleId);
        if (!moduleExistsResult.IsSuccess || !moduleExistsResult.Data)
        {
            _logger.LogWarning("Модуль с ID {ModuleId} не найден в курсе с ID {CourseId}.", moduleId, courseId);
            return Result.Failure($"Модуль с ID {moduleId} не найден в курсе с ID {courseId}.");
        }

        try
        {
            var addOrUpdateResult = await testingRepository.AddOrUpdateTestAsync(courseId, moduleId, testQuestions);
            if (addOrUpdateResult.IsSuccess)
            {
                _logger.LogInformation("Тест для курса ID {CourseId}, модуля ID {ModuleId} успешно добавлен или обновлен.", courseId, moduleId);
                return Result.Success();
            }

            _logger.LogWarning("Не удалось добавить или обновить тест для курса ID {CourseId}, модуля ID {ModuleId}.", courseId, moduleId);
            return Result.Failure("Не удалось добавить или обновить тест.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении или обновлении теста для курса ID {CourseId}, модуля ID {ModuleId}.", courseId, moduleId);
            return Result.Failure($"Произошла ошибка при добавлении или обновлении теста: {ex.Message}");
        }
    }
    
    public async Task<Result> DeleteTest(int courseId, int moduleId)
    {
        _logger.LogInformation("Удаление теста. Курс ID: {CourseId}, Модуль ID: {ModuleId}", courseId, moduleId);

        var module = await moduleRepository.GetByIdAsync(moduleId);
        if (module == null)
        {
            _logger.LogWarning("Модуль с ID {ModuleId} не найден.", moduleId);
            return Result.Failure("Модуль не найден.");
        }

        return await testingRepository.DeleteTestAsync(courseId, moduleId);
    }

}
