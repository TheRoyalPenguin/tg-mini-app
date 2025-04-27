using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class AdminService(IUnitOfWork uow, IMapper mapper) : IAdminService
{
    public async Task<Result> RegisterUserOnCourse(int userId, int courseId)
    {
        await uow.StartTransactionAsync();

        var newEnrollment = new Enrollment
        {
            CourseId = courseId,
            UserId = userId,
            IsCourseCompleted = false,
            EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
        };
        
        var addEnrolmentResult = await uow.Enrollments.AddAsync(newEnrollment);
        if (!addEnrolmentResult.IsSuccess)
        {
            await uow.RollbackTransactionAsync();
            return Result.Failure(addEnrolmentResult.ErrorMessage!);
        }
        
        var addModuleAccessesResult = await uow.ModuleAccesses.AddAccessesForEveryModuleForUserAsync(userId, courseId);
        if (!addModuleAccessesResult.IsSuccess)
        {
            await uow.RollbackTransactionAsync();
            return Result.Failure(addModuleAccessesResult.ErrorMessage!);
        }
        
        await uow.CommitTransactionAsync();
        
        return Result.Success();
    }

    public async Task<Result<ICollection<(User user, ICollection<TestResult> testResults)>>> GetTestResultsByCourse(int courseId)
    {
        var testResultsResult = await uow.TestResults.GetAllByCourse(courseId);
        if (!testResultsResult.IsSuccess)
            return Result<ICollection<(User user, ICollection<TestResult> testResults)>>
                .Failure(testResultsResult.ErrorMessage!)!;
        
        var usersResult = await uow.Users.GetAllByCourseIdAsync(courseId);
        if (!usersResult.IsSuccess)
            return Result<ICollection<(User user, ICollection<TestResult> testResults)>>
                .Failure(usersResult.ErrorMessage!)!;

        try
        {
            var users = usersResult.Data;
            var testResults = testResultsResult.Data;

            var usersAndTests = users.Select(user => 
                (user, (ICollection<TestResult>)testResults.Where(tr => tr.UserId == user.Id).ToList())
            ).ToList();
            
            return Result<ICollection<(User user, ICollection<TestResult> testResults)>>.Success(usersAndTests);
        }
        catch (Exception e)
        {
            return Result<ICollection<(User user, ICollection<TestResult> testResults)>>
                .Failure(e.Message)!;
        }
    }

    public async Task<Result<(User user, ICollection<TestResult> testResults)>> GetTestResultsByUser(int userId)
    {
        var testResultsResult = await uow.TestResults.GetAllByUser(userId);
        if (!testResultsResult.IsSuccess)
            return Result<(User user, ICollection<TestResult> testResults)>
                .Failure(testResultsResult.ErrorMessage!)!;

        var userResult = await uow.Users.GetByIdAsync(userId);
        if (!userResult.IsSuccess)
            return Result<(User user, ICollection<TestResult> testResults)>
                .Failure(userResult.ErrorMessage!)!;

        try
        {
            var user = userResult.Data;
            var testResults = testResultsResult.Data;
            
            return Result<(User user, ICollection<TestResult> testResults)>
                .Success((user, testResults));
        }
        catch (Exception e)
        {
            return Result<(User user, ICollection<TestResult> testResults)>
                .Failure(e.Message)!;
        }
    }

    public async Task<Result<(User user, ICollection<TestResult> testResults)>> GetTestResultsForUserByCourse(int userId, int courseId)
    {
        var testResultsResult = await uow.TestResults.GetAllForUserByCourse(userId, courseId);
        if (!testResultsResult.IsSuccess)
            return Result<(User user, ICollection<TestResult> testResults)>
                .Failure(testResultsResult.ErrorMessage!)!;

        var userResult = await uow.Users.GetByIdAsync(userId);
        if (!userResult.IsSuccess)
            return Result<(User user, ICollection<TestResult> testResults)>
                .Failure(userResult.ErrorMessage!)!;

        try
        {
            var user = userResult.Data;
            var testResults = testResultsResult.Data;
            
            return Result<(User user, ICollection<TestResult> testResults)>
                .Success((user, testResults));
        }
        catch (Exception e)
        {
            return Result<(User user, ICollection<TestResult> testResults)>
                .Failure(e.Message)!;
        }
    }
}